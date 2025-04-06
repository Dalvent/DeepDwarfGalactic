using System;
using UnityEngine;

namespace Script.Interact
{
    public interface IInteractable
    {
        void Interact(DwarfInteraction dwarf);
        PopupInfo GetPopupInfo();
        InteractCooldownInfo GetInteractCooldown();
    }

    [Serializable]
    public class InteractCooldownInfo
    {
        public string CooldownGroupKey;
        public float CooldownTime;
    }
    
    public class PopupInfo
    {
        public IInteractable Interactable { get; set; }
        public Transform Transform { get; set; }
        public string PopupText { get; set; }
        public Vector2 Offset { get; set; }
    }
}