using UnityEngine;

[System.Serializable]
public class RailForEnemies : MonoBehaviour
{
    public Vector2 Rail;

    public float FromX => transform.position.x + Rail.x;
    public float ToX => transform.position.x + Rail.y;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Позиция начала и конца рельсы по Y
        Vector3 from = new Vector3(FromX, transform.position.y, transform.position.z);
        Vector3 to = new Vector3(ToX, transform.position.y, transform.position.z);

        // Рельса — линия
        Gizmos.DrawLine(from, to);

        // Концы рельсы как точки
        Gizmos.DrawSphere(from, 0.05f);
        Gizmos.DrawSphere(to, 0.05f);
    }
}