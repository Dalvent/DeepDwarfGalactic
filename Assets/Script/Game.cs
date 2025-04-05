using Script;
using UnityEngine;

public class Game : MonoBehaviour
{
    private GameStats _gameStats;
    
    public GameUI GameUI;
    public PopupManager PopupManager;
    public GameSettings GameSettings;
    public static Game Instance { get; private set; }

    private float Diamonds
    {
        get => _gameStats.Diamonds;
        set
        {
            if (_gameStats.Diamonds == value)
                return;

            _gameStats.Diamonds = value;
            GameUI.UpdateDiamonds(_gameStats.Diamonds);
        }
    }
    
    private float DrillFuel
    {
        get => _gameStats.DrillFuel;
        set
        {
            if (_gameStats.DrillFuel == value)
                return;

            _gameStats.DrillFuel = value;
            GameUI.UpdateFuel(_gameStats.DrillFuel);
        }
    }

    public bool ThrowDiamondToFurnace()
    {
        if (_gameStats.Diamonds <= 0)
            return false;
        
        Diamonds--;
        DrillFuel += GameSettings.FuelPerDiamond;
        return true;
    }
    
    void Awake()
    {
        Instance = this;
        _gameStats = new GameStats()
        {
            Depth = GameSettings.StartDepth,
            Diamonds = GameSettings.StartDiamond,
            DrillFuel = GameSettings.StartFuel,
            DwarfHP = GameSettings.StartHP
        };
        
        GameUI.UpdateDepth(_gameStats.Depth);
        GameUI.UpdateDiamonds(_gameStats.Diamonds);
        GameUI.UpdateFuel(_gameStats.DrillFuel);
    }
}
