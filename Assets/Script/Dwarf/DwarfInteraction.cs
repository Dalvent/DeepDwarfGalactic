using System.Linq;
using Script;
using Script.Interact;
using UnityEngine;
using UnityEngine.Serialization;

public class DwarfInteraction : MonoBehaviour
{
    [FormerlySerializedAs("dwarfSpecialMoves")] [FormerlySerializedAs("DwarfStateWithAnimation")] public DwarfSpecialMovesWithAnimation dwarfSpecialMovesWithAnimation;
    public float InteractRadius = 2f;
    public LayerMask InDrillInteractableLayer;
    public LayerMask OutDrillInteractableLayer;

    private IInteractable _currentInteractable;

    public bool IsInDrill { get; set; }

    private LayerMask CurrentInteractableLayer => IsInDrill 
        ? InDrillInteractableLayer
        : OutDrillInteractableLayer;

    void Update()
    {
        // Поиск интерактивных объектов
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, InteractRadius, CurrentInteractableLayer);
        _currentInteractable = null;
        
        var hit = hits
            .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
            .FirstOrDefault();
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
            InteractCooldownInfo cooldownInfo = _currentInteractable.GetInteractCooldown();
            InteractCooldownService cooldownService = Game.Instance.InteractCooldownService;
            
            if (cooldownService.CanInteract(cooldownInfo.CooldownGroupKey))
            {
                _currentInteractable.Interact(this);
                cooldownService.MakeCooldown(cooldownInfo);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, InteractRadius);
    }
}