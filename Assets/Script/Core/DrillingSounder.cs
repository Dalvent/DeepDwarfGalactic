using UnityEngine;

public class DrillingSounder : MonoBehaviour
{
    public AudioSource DrillAudio;

    [Header("Pitch настройки")]
    public float MinPitch = 0.7f;
    public float MaxPitch = 3f;
    
    [Header("Громкость по позиции игрока")]
    public DwarfMovement Dwarf;
    public float fadeStartY = 10f;
    public float fadeEndY = 20f;
    public float maxVolume = 0.4f; 
    
    void Update()
    {
        var currentSpeed = Game.Instance.DrillAccelerator.CurrentSpeed;
        var maxSpeed = Game.Instance.GameSettings.MaxSpeed;
        
        float speedRatio = Mathf.Clamp01(currentSpeed / maxSpeed);
        float targetPitch = Mathf.Lerp(MinPitch, MaxPitch, speedRatio);
        DrillAudio.pitch = Mathf.Lerp(DrillAudio.pitch, targetPitch, Time.deltaTime * 8f);

        float distanceY = Mathf.Abs(Dwarf.transform.position.y - transform.position.y);

        float fadeT = Mathf.InverseLerp(fadeStartY, fadeEndY, distanceY);

        float volume = Mathf.Clamp01(1f - fadeT) * maxVolume;

        DrillAudio.volume = volume;
    }
}
