﻿using Script.Helpers;
using Script.Interact;
using UnityEngine;
using UnityEngine.Serialization;

public class Hatch : MonoBehaviour, IInteractable
{
    public Transform DrillFloor;
    
    public Vector2 PopupOffset;
    public string PopupText;
    [FormerlySerializedAs("InteractCooldown")] public InteractCooldownInfo interactCooldownInfo;
    public AudioSource OpenHatch;
    
    public void Interact(DwarfInteraction dwarf)
    {
        dwarf.dwarfSpecialMovesWithAnimation.StayInDrill(DrillFloor.position);
        OpenHatch.PlayWithRandomPitch();
    }

    public PopupInfo GetPopupInfo()
    {
        return new PopupInfo
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