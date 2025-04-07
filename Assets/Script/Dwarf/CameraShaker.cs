using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShaker : MonoBehaviour
{
    // Исходное положение камеры.
    private Vector3 originalPos;

    // Интенсивность тряски (настраивается через инспектор).
    public float shakeMagnitude = 0.2f;
    
    // Скорость возвращения камеры в исходное положение после остановки тряски.
    public float returnSpeed = 5.0f;

    // Флаг, определяющий, происходит ли тряска.
    public bool IsShaking;

    void Start()
    {
        // Сохраняем начальное положение камеры.
        originalPos = transform.localPosition;
    }

    void LateUpdate()
    {
        if (IsShaking)
        {
            // Применяем случайное смещение.
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;
        }
        else
        {
            // Плавно возвращаем камеру к исходному положению.
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * returnSpeed);
        }
    }
}
