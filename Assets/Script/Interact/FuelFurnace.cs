using Script.Interact;
using UnityEngine;

public class FuelFurnace : MonoBehaviour, IInteractable
{
    public Vector2 PopupOffset;
    public string PopupText = "Fuel Furnace";
    
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
            PopupText = PopupText
        };
    }
}
