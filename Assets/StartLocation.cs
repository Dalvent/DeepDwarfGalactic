using UnityEngine;

public class StartLocation : MonoBehaviour
{
    public Sprite[] NewDrillSrites;
    public TargetSpawnNitro TargetSpawnNitro;
    
    public DrillAnimator Driller;

    public void Update()
    {
        if (transform.position.y > Driller.transform.position.y)
        {
            Game.Instance.NitroSpawner.TargetSpawnNitro = TargetSpawnNitro;
            Driller.DrillSprites = NewDrillSrites;
            gameObject.SetActive(false);
        }
    }
}
