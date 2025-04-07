using UnityEngine;

public class WinGameTrigger : MonoBehaviour
{
    public DwarfMovement Dwarf;

    public void Update()
    {
        if (transform.position.y > Dwarf.transform.position.y)
        {
            Game.Instance.GameUI.GameEndScreen.Show();
            gameObject.SetActive(false);
        }
    }
}
