using System.Collections;
using UnityEngine;

public class DwarfSpecialMoves : MonoBehaviour
{
    public DwarfMovement DwarfMovement;
    public DwarfInteraction DwarfInteraction;
    public DwarfLadderMovement DwarfLadderMovement;

    public void Awake()
    {
        DwarfLadderMovement.enabled = false;
        DwarfInteraction.IsInDrill = true;
    }

    public void UseLadder(Vector3 bottomLadderPosition)
    {
        DwarfMovement.EnableInput = false;
        DwarfLadderMovement.enabled = true;
        
        transform.position = bottomLadderPosition;
        DwarfInteraction.IsInDrill = false;
    }
    
    public void UseLadderOutDrill(Vector3 topLadderPosition)
    {
        DwarfMovement.EnableInput = false;
        DwarfLadderMovement.enabled = true;

        transform.position = topLadderPosition;
        DwarfInteraction.IsInDrill = false;
    }

    public void TakeOutOfDrill(Vector3 drillRoof)
    {
        DwarfMovement.EnableInput = true;
        DwarfLadderMovement.enabled = false;
        
        transform.position = drillRoof;
        DwarfInteraction.IsInDrill = false;   
    }

    public void StayInDrill(Vector3 drillFloor)
    {
        DwarfMovement.EnableInput = true;
        DwarfLadderMovement.enabled = false;
        
        transform.position = drillFloor;
        DwarfInteraction.IsInDrill = true;
    }

    public void InteractWithFurnace()
    {
        IEnumerator Use()
        {
            DwarfInteraction.enabled = false;
            DwarfMovement.EnableInput = false;
            yield return new WaitForSeconds(1f);
            DwarfInteraction.enabled = true;
            DwarfMovement.EnableInput = true;
        }

        StartCoroutine(Use());
    }
}