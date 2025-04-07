using System;
using Script.Interact;
using UnityEngine;
using UnityEngine.Serialization;

public class NitroDrillButton : MonoBehaviour, IInteractable
{
    public Vector2 PopupOffset;
    public string PopupText = "Start/Stop Nitro";

    [FormerlySerializedAs("InteractCooldown")] public InteractCooldownInfo interactCooldownInfo;
    
    public SpriteRenderer SpriteRenderer;
    public Sprite ActiveSprite;
    public Sprite DisableSprite;

    private void Start()
    {
        SyncSprites();
    }

    private void Update()
    {
        if (Game.Instance.DrillAccelerator.NitroFuel <= 0)
        {
            Game.Instance.DrillAccelerator.UseNitro = !Game.Instance.DrillAccelerator.UseNitro;
            SyncSprites();
        }
    }

    public void Interact(DwarfInteraction dwarf)
    {
        if (Game.Instance.DrillAccelerator.NitroFuel <= 0)
            return;
        
        Game.Instance.DrillAccelerator.UseNitro = !Game.Instance.DrillAccelerator.UseNitro;
        SyncSprites();
    }

    public PopupInfo GetPopupInfo()
    {
        return new PopupInfo()
        {
            Offset = PopupOffset,
            PopupText = PopupText,
            Transform = transform,
            Interactable = this
        };
    }

    public InteractCooldownInfo GetInteractCooldown()
    {
        return interactCooldownInfo;
    }

    private void SyncSprites()
    {
        SpriteRenderer.sprite = Game.Instance.DrillAccelerator.UseNitro ? ActiveSprite : DisableSprite;
    }
}