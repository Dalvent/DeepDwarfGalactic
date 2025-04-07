using Script.Helpers;
using Script.Interact;
using UnityEngine;
using UnityEngine.Serialization;

public class Ladder : MonoBehaviour, IInteractable
{
    public Transform OutDrill;
    public Vector2 PopupOffset;
    public string PopupText;
    public AudioSource OpenHatch;

    [FormerlySerializedAs("InteractCooldown")] public InteractCooldownInfo interactCooldownInfo;
    
    public void Interact(DwarfInteraction dwarf)
    {
        dwarf.dwarfSpecialMovesWithAnimation.TakeOutOfDrill(OutDrill.position);
        OpenHatch.PlayWithRandomPitch();
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
}
