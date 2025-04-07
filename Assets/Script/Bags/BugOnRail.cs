using UnityEngine;

public class BugOnRail : MonoBehaviour
{
    public RailForEnemies Rail;
    public BugRotator BugRotator;
    public float Speed = 1f;

    public void Update()
    {
        var moveVector = BugRotator.IsLookForward ? Vector3.right : Vector3.left;
        transform.position += moveVector * (Speed * Time.deltaTime);

        if (BugRotator.IsLookForward)
        {
            if (transform.position.x > Rail.ToX)
                BugRotator.IsLookForward = false;
        }
        else
        {
            if (transform.position.x < Rail.FromX)
                BugRotator.IsLookForward = true;
        }
    }
}