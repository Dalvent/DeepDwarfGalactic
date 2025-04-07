using System;
using UnityEngine;

public class CameraOperator : MonoBehaviour
{
    public CameraShaker CameraShaker;
    public Transform Player;

    private bool followY = false;
    
    [Header("Follow")]
    public float YLock = 0f;
    public float YCanGo = 0f;
    public float FollowSpeed = 3f;
    
    [Header("Shake")]
    public float ShakeReducer = 0.2f;
    public float ShakeFullPowerY = 10f;
    public float ShakeIgnoreY = 20f;
    
    [Header("ShakeAfterUser")]
    public float ShakeAfterNitroTime = 0.12f;
    public float ShakeAfterPower = 0.21f;

    private float _forceShakeTime;
    private bool _lastNitroStatus = false;
    
    private void Update()
    {
        if (UseForce()) 
            return;

        float shake = Game.Instance.DrillAccelerator.CurrentSpeed / Game.Instance.GameSettings.MaxSpeed * ShakeReducer;
        if (Player.position.y > ShakeIgnoreY)
        {
            shake = 0;
        }
        else if (Player.position.y > ShakeFullPowerY)
        {
            var reducer = (Player.position.y - ShakeFullPowerY) / (ShakeIgnoreY - ShakeFullPowerY);
            shake *= reducer;
        }
        
        CameraShaker.shakeMagnitude = shake;
        CameraShaker.IsShaking = true;
    }

    void LateUpdate()
    {
        LateUpdateFollowY();
    }

    private bool UseForce()
    {
        if (_lastNitroStatus == false && Game.Instance.DrillAccelerator.UseNitro)
        {
            _forceShakeTime = ShakeAfterNitroTime;
        }

        _lastNitroStatus = Game.Instance.DrillAccelerator.UseNitro;

        if (_forceShakeTime > 0)
        {
            CameraShaker.IsShaking = true;
            CameraShaker.shakeMagnitude = ShakeAfterPower;
            _forceShakeTime -= Time.deltaTime;
            return true;
        }

        return false;
    }

    private void LateUpdateFollowY()
    {
        Vector3 targetPos = transform.position;

        followY = Player.position.y > YCanGo;
        if (followY)
        {
            float targetY = Player.position.y;
            if (MathF.Abs(transform.position.y - targetY) < 0.01f)
                targetPos.y = targetY;
            else
                targetPos.y = Mathf.Lerp(transform.position.y, targetY, FollowSpeed * Time.deltaTime);
        }
        else
        {
            targetPos.y = Mathf.Lerp(transform.position.y, YLock, FollowSpeed * Time.deltaTime);
        }

        transform.position = targetPos;
    }
}