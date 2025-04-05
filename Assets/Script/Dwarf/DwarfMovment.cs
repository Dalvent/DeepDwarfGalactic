using UnityEngine;

public class DwarfMovment : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D Rigidbody2D;
    public float MoveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private float moveInput;

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocityX, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Ходьба
        Rigidbody2D.linearVelocity = new Vector2(moveInput * MoveSpeed, Rigidbody2D.linearVelocityY);

        // Проверка земли
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
