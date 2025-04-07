using UnityEngine;

public class BugBouncer : MonoBehaviour
{
    public Vector2 CapsuleSize = new(1f, 0.5f);
    public Vector2 CapsuleOffset = new(0f, 0.5f);
    public CapsuleDirection2D CapsuleDirection = CapsuleDirection2D.Vertical;
    public float BounceForce = 10f;
    public LayerMask PlayerLayer;
    public float BounceDelayForPlayer;

    private DwarfMovement _targetDwarfMovement;
    
    void FixedUpdate()
    {
        Vector2 capsuleCenter = (Vector2)transform.position + CapsuleOffset;

        Collider2D player = Physics2D.OverlapCapsule(
            capsuleCenter,
            CapsuleSize,
            CapsuleDirection,
            0f,
            PlayerLayer
        );

        if (player == null)
        {
            _targetDwarfMovement = null;
            return;
        }

        var dwarfMovement = player.GetComponent<DwarfMovement>();
        if (_targetDwarfMovement != dwarfMovement)
        {
            if (dwarfMovement != null)
                dwarfMovement.Bounce(BounceForce, BounceDelayForPlayer);
            
            _targetDwarfMovement = dwarfMovement;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector2 capsuleCenter = (Vector2)transform.position + CapsuleOffset;

#if UNITY_EDITOR
        // Рисуем приближённую капсулу как комбинацию прямоугольника и кружков
        float radius, height;
        if (CapsuleDirection == CapsuleDirection2D.Vertical)
        {
            radius = CapsuleSize.x / 2f;
            height = CapsuleSize.y;
        }
        else
        {
            radius = CapsuleSize.y / 2f;
            height = CapsuleSize.x;
        }

        // Центр тела капсулы
        float bodyHeight = Mathf.Max(0, height - 2 * radius);
        Vector3 size = CapsuleDirection == CapsuleDirection2D.Vertical
            ? new Vector3(radius * 2, bodyHeight)
            : new Vector3(bodyHeight, radius * 2);

        Gizmos.DrawWireCube(capsuleCenter, size);

        // Верхний и нижний кружки
        if (CapsuleDirection == CapsuleDirection2D.Vertical)
        {
            Gizmos.DrawWireSphere(capsuleCenter + Vector2.up * bodyHeight / 2, radius);
            Gizmos.DrawWireSphere(capsuleCenter + Vector2.down * bodyHeight / 2, radius);
        }
        else
        {
            Gizmos.DrawWireSphere(capsuleCenter + Vector2.right * bodyHeight / 2, radius);
            Gizmos.DrawWireSphere(capsuleCenter + Vector2.left * bodyHeight / 2, radius);
        }
#endif
    }
}
