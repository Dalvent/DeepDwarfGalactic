using UnityEngine;

public class DwarCollector : MonoBehaviour
{
    public float CollectRadius = 1f;
    public LayerMask CollectorLayer;

    void Update()
    {
        // Поиск интерактивных объектов
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, CollectRadius, CollectorLayer);

        foreach (var hit in hits)
        {
            var collectable = hit.GetComponent<ICollectable>();
            collectable.Collect();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, CollectRadius);
    }
}
