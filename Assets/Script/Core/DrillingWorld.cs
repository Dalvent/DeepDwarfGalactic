using Script.Helpers;
using UnityEngine;

public class DrillingWorld : MonoBehaviour
{
    public CameraShaker CameraShaker;
    private float _startY;
    public float ShakeReducer = 0.2f;

    [Header("ShakeAfterUser")]
    public float ShakeAfterNitroTime = 0.12f;
    public float ShakeAfterPower = 0.21f;

    private float _forceShakeTime;
    private bool _lastNitroStatus = false;
    
    public void Awake()
    {
        _startY = transform.position.y;
    }

    public void Update()
    {
        if (_lastNitroStatus == false && Game.Instance.DrillAccelerator.UseNitro)
        {
            _forceShakeTime = ShakeAfterNitroTime;
        }
        
        _lastNitroStatus = Game.Instance.DrillAccelerator.UseNitro;


        float depth = Game.Instance.Depth + _startY - Game.Instance.GameSettings.StartDepth;
        transform.position = transform.position.SetY(depth);

        if (_forceShakeTime <= 0)
        {
            CameraShaker.shakeMagnitude = Game.Instance.DrillAccelerator.CurrentSpeed / Game.Instance.GameSettings.MaxSpeed * ShakeReducer;
            CameraShaker.IsShaking = true;
        }
        else
        {
            CameraShaker.shakeMagnitude = ShakeAfterPower;
            _forceShakeTime -= Time.deltaTime;
        }
    }
}
