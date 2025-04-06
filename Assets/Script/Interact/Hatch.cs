﻿using Script.Interact;
using UnityEngine;

public class Hatch : MonoBehaviour, IInteractable
{
    public Transform DrillFloor;
    
    public Vector2 PopupOffset;
    public string PopupText;
    
    public void Interact(DwarfInteraction dwarf)
    {
        dwarf.dwarfSpecialMovesWithAnimation.StayInDrill(DrillFloor.position);
    }

    public PopupInfo GetPopupInfo()
    {
        return new PopupInfo
        {
            Offset = PopupOffset,
            PopupText = PopupText,
            Transform = transform
        };
    }
}