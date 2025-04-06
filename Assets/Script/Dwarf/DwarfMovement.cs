using System;
using Script.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

public class DwarfMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public CapsuleCollider2D col;
    [FormerlySerializedAs("animator")] public Animator Animator; // Если аниматор не нужен – можно оставить пустым

    [Header("Movement Settings")]
    [Tooltip("Максимальная скорость по горизонтали")]
    public float maxSpeed = 14f;
    [Tooltip("Ускорение при движении")]
    public float acceleration = 120f;
    [Tooltip("Торможение на земле (без скольжения)")]
    public float groundDeceleration = 999f; // большое значение для мгновенного торможения
    [Tooltip("Замедление в воздухе при отсутствии ввода")]
    public float airDeceleration = 30f;

    [Header("Jump Settings")]
    [Tooltip("Сила прыжка (начальная вертикальная скорость)")]
    public float jumpPower = 36f;
    [Tooltip("Время (сек), в течение которого можно прыгнуть после ухода с земли (coyote time)")]
    public float coyoteTime = 0.15f;
    [Tooltip("Время (сек) буфера для прыжка")]
    public float jumpBufferTime = 0.2f;

    [Header("Gravity Settings")]
    [Tooltip("Постоянная сила при соприкосновении с землей (для устойчивости на наклонах)")]
    public float groundingForce = -1.5f;
    [Tooltip("Ускорение падения в воздухе")]
    public float fallAcceleration = 110f;
    [Tooltip("Максимальная скорость падения")]
    public float maxFallSpeed = 40f;
    [Tooltip("Множитель гравитации, когда прыжок прерывается (отпустили кнопку)")]
    public float jumpEndEarlyGravityModifier = 3f;

    [Header("Collision Settings")]
    [Tooltip("Точка проверки земли (обычно пустой объект под персонажем)")]
    public Transform groundCheck;
    [Tooltip("Радиус проверки земли")]
    public float groundCheckRadius = 0.05f;
    [Tooltip("Слой, определяющий землю/платформы")]
    public LayerMask groundLayer;

    [Header("Input Settings")]
    [Tooltip("Включает округление входного значения до целого (удобно для контроллеров)")]
    public bool snapInput = true;
    [Tooltip("Порог по горизонтали для срабатывания ввода")]
    public float horizontalDeadZoneThreshold = 0.1f;
    [Tooltip("Порог по вертикали для срабатывания ввода")]
    public float verticalDeadZoneThreshold = 0.3f;

    public bool EnableInput = true;

    // Внутреннее состояние
    private Vector2 frameVelocity; // вычисленная скорость, которую мы применим к Rigidbody2D
    private bool isGrounded;
    private float time;
    private float timeLeftGrounded = float.MinValue;

    private bool jumpToConsume;
    private bool bufferedJumpUsable = true;
    private bool endedJumpEarly;
    private bool coyoteUsable;
    private float timeJumpWasPressed;

    // Внутренняя структура ввода
    private struct FrameInput
    {
        public bool jumpDown;
        public bool jumpHeld;
        public Vector2 move;
    }
    private FrameInput frameInput;

    private void Update()
    {
        time += Time.deltaTime;
        GatherInput();
        UpdateAnimator();
    }

    private void GatherInput()
    {
        if (EnableInput)
        {
            frameInput.jumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C);
            frameInput.jumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C);
            frameInput.move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            frameInput.jumpDown = false;
            frameInput.jumpHeld = false;
            frameInput.move = Vector2.zero;
        }

        if (Math.Abs(frameInput.move.x - Vector2.zero.x) > 0.00001f)
        {
            if (frameInput.move.x > 0f)
                transform.localScale = transform.localScale.NegX();
            else
                transform.localScale = transform.localScale.PosX();
        }
            

        if (snapInput)
        {
            frameInput.move.x = Mathf.Abs(frameInput.move.x) < horizontalDeadZoneThreshold ? 0 : Mathf.Sign(frameInput.move.x);
            frameInput.move.y = Mathf.Abs(frameInput.move.y) < verticalDeadZoneThreshold ? 0 : Mathf.Sign(frameInput.move.y);
        }

        if (frameInput.jumpDown)
        {
            jumpToConsume = true;
            timeJumpWasPressed = time;
        }
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        HandleJump();
        HandleDirection();
        HandleGravity();
        ApplyMovement();
    }

    private void CheckCollisions()
    {
        // Простой способ проверки земли с помощью OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Если персонаж только что коснулся земли, сбрасываем буферы
        if (isGrounded)
        {
            coyoteUsable = true;
            bufferedJumpUsable = true;
            endedJumpEarly = false;
        }
        // Если оторвались от земли – фиксируем время
        else if (!isGrounded && rb.linearVelocity.y <= 0 && timeLeftGrounded == float.MinValue)
        {
            timeLeftGrounded = time;
        }
    }

    private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpWasPressed + jumpBufferTime;
    private bool CanUseCoyote => coyoteUsable && (!isGrounded) && (time < timeLeftGrounded + coyoteTime);

    private void HandleJump()
    {
        // Если кнопку прыжка отпустили до достижения пика, увеличиваем гравитацию
        if (!endedJumpEarly && !isGrounded && !frameInput.jumpHeld && rb.linearVelocity.y > 0)
        {
            endedJumpEarly = true;
        }

        if (!jumpToConsume && !HasBufferedJump)
            return;

        if (isGrounded || CanUseCoyote)
        {
            ExecuteJump();
            // Сбросим время отслеживания отрыва от земли после прыжка
            timeLeftGrounded = float.MinValue;
        }
        jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        endedJumpEarly = false;
        timeJumpWasPressed = 0;
        bufferedJumpUsable = false;
        coyoteUsable = false;
        frameVelocity.y = jumpPower;
        // Триггер анимации прыжка
        if (Animator != null)
            Animator.SetTrigger("Jump");
    }

    private void HandleDirection()
    {
        // Если нет ввода по горизонтали и персонаж на земле – мгновенно обнуляем скорость (без скольжения)
        if (Mathf.Abs(frameInput.move.x) < 0.01f && isGrounded)
        {
            frameVelocity.x = 0;
        }
        else if (Mathf.Abs(frameInput.move.x) < 0.01f && !isGrounded)
        {
            // В воздухе – замедление происходит медленнее
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, airDeceleration * Time.fixedDeltaTime);
        }
        else
        {
            // С ускорением к целевой скорости
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, frameInput.move.x * maxSpeed, acceleration * Time.fixedDeltaTime);
        }
    }

    private void HandleGravity()
    {
        if (isGrounded && frameVelocity.y <= 0)
        {
            frameVelocity.y = groundingForce;
        }
        else
        {
            float inAirGravity = fallAcceleration;
            if (endedJumpEarly && frameVelocity.y > 0)
                inAirGravity *= jumpEndEarlyGravityModifier;
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    private void ApplyMovement()
    {
        rb.linearVelocity = frameVelocity;
    }

    private void UpdateAnimator()
    {
        if (Animator == null)
            return;

        Animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        Animator.SetBool("IsGrounded", isGrounded);
        Animator.SetFloat("VerticalSpeed", rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
