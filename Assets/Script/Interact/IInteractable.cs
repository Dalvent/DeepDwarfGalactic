using UnityEngine;

namespace Script.Interact
{
    public interface IInteractable
    {
        void Interact();
        PopupInfo GetPopupInfo();
    }
    
    public class PopupInfo
    {
        public Transform Transform { get; set; }
        public string PopupText { get; set; }
        public Vector2 Offset { get; set; }
    }
}