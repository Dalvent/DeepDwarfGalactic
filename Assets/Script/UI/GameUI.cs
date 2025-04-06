using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Slider FuelSlider;
    public TMP_Text DiamondsText;
    
    [Header("Depth")]
    public Slider DepthSlider;
    public TMP_Text DepthText;

    public void UpdateFuel(float fuel)
    {
        FuelSlider.value = fuel / Game.Instance.GameSettings.MaxFuel;
    }
    
    public void UpdateDiamonds(float diamonds)
    {
        DiamondsText.text = $"{diamonds}";
    }

    public void UpdateDepth(float depth)
    {
        DepthText.text = $"{(int)depth} m";
        DepthSlider.value = depth / Game.Instance.GameSettings.MaxDepth;
    }
}