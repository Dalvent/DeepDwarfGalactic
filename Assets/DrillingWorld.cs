using Script.Helpers;
using UnityEngine;

public class DrillingWorld : MonoBehaviour
{
    public CameraShaker CameraShaker;
    private float _startY;
    public float ShakeReducer = 0.2f;
    public bool UseNitro { get; set; }

    public void Awake()
    {
        _startY = transform.position.y;
    }

    public void Update()
    {
        float depth = Game.Instance.Depth + _startY - Game.Instance.GameSettings.StartDepth;
        transform.position = transform.position.SetY(depth);
        
        bool isSpeedFast = Game.Instance.GameSettings.MaxSpeed / 2 < Game.Instance.DrillAccelerator.CurrentSpeed;

        ShakeReducer = 0.2f;
        CameraShaker.shakeMagnitude = Game.Instance.DrillAccelerator.CurrentSpeed / Game.Instance.GameSettings.MaxSpeed * ShakeReducer; 
        CameraShaker.IsShaking = true;
    }
}
