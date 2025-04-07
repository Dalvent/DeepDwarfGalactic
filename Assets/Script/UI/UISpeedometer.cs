using UnityEngine;

public class UISpeedometer : MonoBehaviour
{
    public RectTransform needle;     // Стрелка
    public float minAngle = -130f;   // Угол на 0 скорости
    public float maxAngle = 130f;    // Угол на max скорости

    void Update()
    {
        if (Game.Instance == null)
            return;
        
        float normalizedSpeed = Mathf.Clamp01(Game.Instance.DrillAccelerator.CurrentSpeed / Game.Instance.GameStats.MaxSpeed);
        float needleAngle = Mathf.Lerp(minAngle, maxAngle, normalizedSpeed);
        needle.localRotation = Quaternion.Euler(0, 0, needleAngle);
    }
}
