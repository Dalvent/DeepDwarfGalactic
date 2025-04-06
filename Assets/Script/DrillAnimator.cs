using System;
using UnityEngine;

public class DrillAnimator : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Sprite[] Sprites;
    public float ChangeSpriteSpeedMultiplayer;

    private int _currentSpriteIndex;
    private float _currentTimer;
    
    private void Update()
    {
        if (_currentTimer >= CalculateNextTimeToSprite())
        {
            _currentSpriteIndex++;
            if (_currentSpriteIndex >= Sprites.Length)
                _currentSpriteIndex = 0;

            SpriteRenderer.sprite = Sprites[_currentSpriteIndex];
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