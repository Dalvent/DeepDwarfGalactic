using Script.Interact;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    public Transform OutDrill;
    public Vector2 PopupOffset;
    public string PopupText;
    
    public void Interact(DwarfInteraction dwarf)
    {
        dwarf.DwarfStateWithAnimation.TakeOutOfDrill(OutDrill.position);
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
