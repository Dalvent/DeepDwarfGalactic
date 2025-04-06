using Script.Interact;
using UnityEngine;

public class NitroDrillButton : MonoBehaviour, IInteractable
{
    public Vector2 PopupOffset;
    public string PopupText = "Start/Stop Nitro";
    
    public void Interact(DwarfInteraction dwarf)
    {
        Game.Instance.DrillAccelerator.UseNitro = !Game.Instance.DrillAccelerator.UseNitro;
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
}