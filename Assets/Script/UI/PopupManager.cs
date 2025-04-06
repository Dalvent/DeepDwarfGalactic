using Script.Helpers;
using Script.Interact;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject PopupUI;

    private PopupInfo _targetInfo;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (_targetInfo != null && _mainCamera != null)
        {
            MovePopupToTarget();
        }
    }

    public void ShowPopup(PopupInfo target)
    {
        _targetInfo = target;
        
        MovePopupToTarget();
        
        PopupUI.SetActive(true);
    }

    public void HidePopup()
    {
        _targetInfo = null;
        PopupUI.SetActive(false);
    }

    private void MovePopupToTarget()
    {
        Vector3 worldPos = _targetInfo.Transform.position + _targetInfo.Offset.ToVector3();
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(worldPos);
        PopupUI.transform.position = screenPos;
    }
}