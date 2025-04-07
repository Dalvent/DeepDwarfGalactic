using UnityEngine;

public class GroundBug : MonoBehaviour
{
    public Rigidbody2D Rb;
    public float MoveSpeed = 2f;
    public float RayDistance = 1f;
    public LayerMask PlayerLayer;
    public LayerMask WallLayer;

    private bool isFacingRight = true;

    void Update()
    {
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        Vector2 origin = transform.position;

        RaycastHit2D playerHit = Physics2D.Raycast(origin, direction, RayDistance, PlayerLayer);
        RaycastHit2D wallHit = Physics2D.Raycast(origin, direction, RayDistance, WallLayer);

        if (wallHit.collider != null)
        {
            Flip();
            return;
        }

        if (playerHit.collider != null)
        {
            Rb.linearVelocity = new Vector2(direction.x * MoveSpeed, Rb.linearVelocity.y);
        }
        else
        {
            Rb.linearVelocity = new Vector2(direction.x * MoveSpeed * 0.5f, Rb.linearVelocity.y);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}