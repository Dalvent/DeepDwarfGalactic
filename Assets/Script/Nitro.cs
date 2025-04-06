using System;
using UnityEngine;
using UnityEngine.Serialization;

public interface ICollectable
{
    void Collect();
}

public class Nitro : MonoBehaviour, ICollectable
{
    public int NitroPower = 10;

    public void Collect()
    {
        Game.Instance.CollectDiamonds(NitroPower);
        Destroy(gameObject);
    }
}
