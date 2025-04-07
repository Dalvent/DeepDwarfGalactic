using Script.Helpers;
using Script.Interact;
using UnityEngine;
using UnityEngine.Serialization;

public class FuelFurnace : MonoBehaviour, IInteractable
{
    public Vector2 PopupOffset;
    public string PopupText = "Fuel Furnace";
    [FormerlySerializedAs("InteractCooldown")] public InteractCooldownInfo interactCooldownInfo;
    public AudioSource FuelSound;
    
    public void Interact(DwarfInteraction dwarf)
    {
        if (Game.Instance.ThrowDiamondToFurnace())
        {
            dwarf.dwarfSpecialMovesWithAnimation.InteractWithFurnace();
            FuelSound.PlayWithRandomPitch();
        }
    }

    public PopupInfo GetPopupInfo()
    {
        return new PopupInfo()
        {
            Transform = transform,
            Offset = PopupOffset,
            PopupText = PopupText,
            Interactable = this
        };
    }

    public InteractCooldownInfo GetInteractCooldown()
    {
        return interactCooldownInfo;
    }
}
