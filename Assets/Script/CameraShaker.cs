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
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Start()
    {
        // Сохраняем начальное положение камеры.
        originalPos = _mainCamera.transform.localPosition;
    }

    void Update()
    {
        if (IsShaking)
        {
            // Применяем случайное смещение.
            _mainCamera.transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;
        }
        else
        {
            // Плавно возвращаем камеру к исходному положению.
            _mainCamera.transform.localPosition = Vector3.Lerp(_mainCamera.transform.localPosition, originalPos, Time.deltaTime * returnSpeed);
        }
    }
}
