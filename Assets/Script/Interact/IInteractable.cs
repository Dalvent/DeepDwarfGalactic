using UnityEngine;

namespace Script.Interact
{
    public interface IInteractable
    {
        void Interact(DwarfInteraction dwarf);
        PopupInfo GetPopupInfo();
        InteractCooldown GetInteractCooldown();
    }

    public class InteractCooldown
    {
        public string CooldownGroupKey { get; set; }
        public float CooldownTime { get; set; }
    }
    
    public class PopupInfo
    {
        public Transform Transform { get; set; }
        public string PopupText { get; set; }
        public Vector2 Offset { get; set; }
    }
}