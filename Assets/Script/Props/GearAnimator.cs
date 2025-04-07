using UnityEngine;

public class GearAnimator : MonoBehaviour
{
    [Header("Gear")]
    public SpriteRenderer GearSr;
    public Sprite[] GearSprites;
    
    [Header("Values")]
    public float ChangeSpriteSpeedMultiplayer;

    private int _currentSpriteIndex;
    private float _currentTimer;
    
    private void Update()
    {
        if (_currentTimer >= CalculateNextTimeToSprite())
        {
            _currentSpriteIndex++;
            if (_currentSpriteIndex >= GearSprites.Length)
                _currentSpriteIndex = 0;
            
            GearSr.sprite = GearSprites[_currentSpriteIndex];

            _currentTimer = 0f;
        }

        _currentTimer += Time.deltaTime;
    }

    private float CalculateNextTimeToSprite()
    {
        float speedPercent = Game.Instance.DrillAccelerator.CurrentSpeed / Game.Instance.GameStats.MaxSpeed;
        return (1 - speedPercent) * ChangeSpriteSpeedMultiplayer;
    }
}