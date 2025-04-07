using System;
using Script.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

public interface ICollectable
{
    void Collect();
}

public class Nitro : MonoBehaviour, ICollectable
{
    public int NitroPower = 10;
    public SpriteRenderer SpriteRenderer;
    public AudioSource TakeNitro;

    public NitroInfo Info { get; set; }

    public void SetInfo(NitroInfo nitroInfo)
    {
        transform.localPosition = new Vector3(nitroInfo.WorldX, nitroInfo.WorldY);
        SpriteRenderer.sprite = nitroInfo.Sprite;
        NitroPower = nitroInfo.Value;

        Info = nitroInfo;
    }

    public void Collect()
    {
        if (!gameObject.activeSelf || Info == null)
            return;
        
        Game.Instance.CollectDiamonds(NitroPower);
        Game.Instance.NitroSpawner.DeleteNitro(Info);
        TakeNitro.PlayWithRandomPitch();
    }
}
