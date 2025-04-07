using Script.Helpers;
using Script.Interact;
using UnityEngine;
using UnityEngine.Serialization;

public class NitroDrillButton : MonoBehaviour, IInteractable
{
    public static NitroDrillButton Button;
    
    public Vector2 PopupOffset;
    public string PopupText = "Start/Stop Nitro";

    [FormerlySerializedAs("InteractCooldown")] public InteractCooldownInfo interactCooldownInfo;
    
    public SpriteRenderer SpriteRenderer;
    public Sprite ActiveSprite;
    public Sprite DisableSprite;

    public AudioSource PullUpSound;
    public AudioSource PullBackSound;

    private void Start()
    {
        Button = this;
        SyncSprites();
    }

    public void Pull()
    {
        Game.Instance.DrillAccelerator.UseNitro = !Game.Instance.DrillAccelerator.UseNitro;
        SyncSprites();
        
        if (Game.Instance.DrillAccelerator.UseNitro)
            PullUpSound.PlayWithRandomPitch();
        else
            PullBackSound.PlayWithRandomPitch();
    }

    public void Interact(DwarfInteraction dwarf)
    {
        if (Game.Instance.DrillAccelerator.NitroFuel <= 0)
            return;
     
        Pull();
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