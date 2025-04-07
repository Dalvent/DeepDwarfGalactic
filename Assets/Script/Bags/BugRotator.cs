using Script.Helpers;
using UnityEngine;

public class BugRotator : MonoBehaviour
{
    public bool IsLookForward
    {
        get => transform.localScale.x > 0;
        set
        {
            if (IsLookForward == value)
                return;

            float x = value 
                ? Mathf.Abs(transform.localScale.x) 
                : -Mathf.Abs(transform.localScale.x);
            
            transform.localScale = transform.localScale.SetX(x);
        }
    }
}