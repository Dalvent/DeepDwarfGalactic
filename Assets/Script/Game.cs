using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    public DrillingWorld DrillingWorld;
    public GameStats GameStats;
    public GameUI GameUI;
    public PopupManager PopupManager;
    public GameSettings GameSettings;
    public DrillAccelerator DrillAccelerator;
    public static Game Instance { get; private set; }

    private float Diamonds
    {
        get => GameStats.Diamonds;
        set
        {
            if (GameStats.Diamonds == value)
                return;

            GameStats.Diamonds = value;
            GameUI.UpdateDiamonds(GameStats.Diamonds);
        }
    }

    public float Depth
    {
        get => GameStats.Depth;
        private set
        {
            if (GameStats.Depth == value)
                return;

            GameStats.Depth = value;
            GameUI.UpdateDepth(GameStats.Depth);
        }
    }
    
    public void Update()
    {
        DrillAccelerator.CalculateInUpdate();
        
        Depth += Time.deltaTime * DrillAccelerator.CurrentSpeed;
    }

    public bool ThrowDiamondToFurnace()
    {
        if (GameStats.Diamonds <= 0)
            return false;
        
        Diamonds--;
        DrillAccelerator.NitroFuel += GameSettings.FuelPerDiamond;
        return true;
    }
    
    void Awake()
    {
        Instance = this;
        GameStats = new GameStats()
        {
            Depth = GameSettings.StartDepth,
            Diamonds = GameSettings.StartDiamond,
            DrillFuel = GameSettings.StartFuel,
            MaxSpeed = GameSettings.MaxSpeed,
            MinSpeed = GameSettings.MinSpeed,
            SpeedLinerAcceleration = GameSettings.SpeedLinerAcceleration,
            SpeedLoseFactor = GameSettings.SpeedLoseFactor,
            DecayExponent = GameSettings.DecayExponent
        };

        DrillAccelerator = new DrillAccelerator(GameStats, GameUI, GameSettings);
        
        GameUI.UpdateDepth(GameStats.Depth);
        GameUI.UpdateDiamonds(GameStats.Diamonds);
        GameUI.UpdateFuel(DrillAccelerator.NitroFuel);
    }

    public void CollectDiamonds(float count)
    {
        Diamonds += count;
    }
}
