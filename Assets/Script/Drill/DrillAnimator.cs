using UnityEngine;
using UnityEngine.Serialization;

public class DrillAnimator : MonoBehaviour
{
    [Header("Drill")]
    public SpriteRenderer DrillSr;
    public Sprite[] DrillSprites;
    
    [Header("Values")]
    public float ChangeSpriteSpeedMultiplayer;
    
    private int _currentSpriteIndex;
    private float _currentTimer;
    
    private void Update()
    {
        if (_currentTimer >= CalculateNextTimeToSprite())
        {
            _currentSpriteIndex++;
            if (_currentSpriteIndex >= DrillSprites.Length)
                _currentSpriteIndex = 0;
            
            DrillSr.sprite = DrillSprites[_currentSpriteIndex];

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