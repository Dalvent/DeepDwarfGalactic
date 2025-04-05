using System.Linq;
using Script.Interact;
using UnityEngine;

public class DwarfInteraction : MonoBehaviour
{
    public float InteractRadius = 2f;
    public LayerMask InteractableLayer;

    private IInteractable _currentInteractable;

    void Update()
    {
        // Поиск интерактивных объектов
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, InteractRadius, InteractableLayer);

        _currentInteractable = null;
        
        Debug.Log($"Hit somethign {hits.Length}!!");
        
        var hit = hits.FirstOrDefault();
        if (hit != null)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _currentInteractable = interactable;
                Game.Instance.PopupManager.ShowPopup(interactable.GetPopupInfo());
            }
        }

        if (_currentInteractable == null)
        {
            Game.Instance.PopupManager.HidePopup();
        }

        if (_currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            _currentInteractable.Interact();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, InteractRadius);
    }
}