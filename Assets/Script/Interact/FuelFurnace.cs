using Script.Interact;
using UnityEngine;
using UnityEngine.Serialization;

public class FuelFurnace : MonoBehaviour, IInteractable
{
    public Vector2 PopupOffset;
    public string PopupText = "Fuel Furnace";
    [FormerlySerializedAs("InteractCooldown")] public InteractCooldownInfo interactCooldownInfo;
    
    public void Interact(DwarfInteraction dwarf)
    {
        // TODO: MAKE BLOCKING INTERACTION
        Debug.Log("SEX WITH FURNACE WTFFFFF!");
        
        if (Game.Instance.ThrowDiamondToFurnace())
            dwarf.dwarfSpecialMovesWithAnimation.InteractWithFurnace();
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
