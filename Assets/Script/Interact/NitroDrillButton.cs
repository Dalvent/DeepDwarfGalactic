using System;
using Script.Interact;
using UnityEngine;
using UnityEngine.Serialization;

public class NitroDrillButton : MonoBehaviour, IInteractable
{
    public Vector2 PopupOffset;
    public string PopupText = "Start/Stop Nitro";

    public SpriteRenderer SpriteRenderer;
    public Sprite ActiveSprite;
    public Sprite DisableSprite;

    private void Start()
    {
        SyncSprites();
    }

    public void Interact(DwarfInteraction dwarf)
    {
        Game.Instance.DrillAccelerator.UseNitro = !Game.Instance.DrillAccelerator.UseNitro;
        SyncSprites();
    }

    public PopupInfo GetPopupInfo()
    {
        return new PopupInfo()
        {
            Offset = PopupOffset,
            PopupText = PopupText,
            Transform = transform
        };
    }

    private void SyncSprites()
    {
        SpriteRenderer.sprite = Game.Instance.DrillAccelerator.UseNitro ? ActiveSprite : DisableSprite;
    }
}