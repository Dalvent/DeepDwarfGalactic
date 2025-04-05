using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Slider FuelSlider;
    public TMP_Text DiamondsText;
    public TMP_Text DepthText;
    
    public void UpdateFuel(float fuel)
    {
        FuelSlider.value = fuel / Game.Instance.GameSettings.MaxFuel;
    }
    
    public void UpdateDiamonds(float diamonds)
    {
        DiamondsText.text = $"Diamonds: {diamonds}";
    }

    public void UpdateDepth(float depth)
    {
        DepthText.text = $"Depth: {depth}";
    }
}