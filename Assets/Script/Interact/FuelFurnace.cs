using Script.Interact;
using UnityEngine;

public class FuelFurnace : MonoBehaviour, IInteractable
{
    public Vector2 PopupOffset;
    public string PopupText = "Fuel Furnace";
    
    public void Interact()
    {
        // TODO: MAKE BLOCKING INTERACTION
        Debug.Log("SEX WITH FURNACE WTFFFFF!");
        Game.Instance.ThrowDiamondToFurnace();
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
