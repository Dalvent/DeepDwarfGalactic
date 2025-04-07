using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[System.Serializable]
public class NitroSpawner : MonoBehaviour
{
    public Transform World;
    
    [Header("Nitro prefabs")]
    public GameObject LowNitro;
    public GameObject NormalNitro;
    public GameObject HeightNitro;

    [Header("Spawn Position")] 
    public Vector2 SpawnX;

    [Header("Random settings")] 
    public float MinDepthSpawn;
    public float MaxDepthSpawn;

    private float _toNextSpawnDepth;
    
    public void Update()
    {
        if (_toNextSpawnDepth < Game.Instance.GameStats.Depth)
        {
            SpawnNitro();
            _toNextSpawnDepth = Game.Instance.GameStats.Depth + Random.Range(MinDepthSpawn, MaxDepthSpawn);
        }
    }

    private void SpawnNitro()
    {
        float fromX = transform.position.y + SpawnX.x;
        float toX = transform.position.y + SpawnX.y;
        float x = Random.Range(fromX, toX);
        
        float y = transform.position.y;

        Instantiate(LowNitro, new Vector3(x, y, 0), Quaternion.identity, World);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Позиция начала и конца рельсы по Y
        Vector3 from = new Vector3(transform.position.x + SpawnX.x, transform.position.y, transform.position.z);
        Vector3 to = new Vector3(transform.position.x + SpawnX.y, transform.position.y, transform.position.z);

        // Рельса — линия
        Gizmos.DrawLine(from, to);

        // Концы рельсы как точки
        Gizmos.DrawSphere(from, 0.05f);
        Gizmos.DrawSphere(to, 0.05f);
    }
}