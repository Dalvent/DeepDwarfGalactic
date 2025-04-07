using UnityEngine;

public class BugAggro : MonoBehaviour
{
    public const float VisionDistance = 15f;
    
    public BugOnRail BugOnRail;
    public BugRotator BugRotator;
    public float AggroSpeed = 3f;
    public float TimeLookingToPlayer = 1f;
    public LayerMask PlayerLayer;
    
    private float _normalSpeed = 0f;
    private float _lookAtPlayerTime;
    
    private bool _isAggro = false;
    private bool IsAggro
    {
        get => _isAggro;
        set
        {
            if (_isAggro == value)
                return;

            _isAggro = value;

            if (_isAggro)
            {
                _lookAtPlayerTime = TimeLookingToPlayer;
                BugOnRail.Speed = AggroSpeed;
                BugOnRail.enabled = false;
            }
            else
            {
                _lookAtPlayerTime = 0;
                BugOnRail.Speed = _normalSpeed;
                BugOnRail.enabled = true;
            }
        }
    }

    private void Start()
    {
        _normalSpeed = BugOnRail.Speed;
    }

    void Update()
    {
        Vector2 origin = transform.position;
        Vector2 direction = BugRotator.IsLookForward ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, VisionDistance, PlayerLayer);
        IsAggro = hit.collider != null;

        if (_lookAtPlayerTime > 0)
        {
            _lookAtPlayerTime -= Time.deltaTime;
            if (_lookAtPlayerTime <= 0)
                BugOnRail.enabled = true;
        }
    }
}