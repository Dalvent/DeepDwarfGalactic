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
    public InteractCooldownService InteractCooldownService = new();
    public GameObject Player;
    public NitroSpawner NitroSpawner;
    public static Game Instance { get; private set; }

    private float _playerStartPos;

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
            GameUI.UpdateDepth(GameStats.Depth - Player.transform.position.y + _playerStartPos);
        }
    }
    
    public void Update()
    {
        DrillAccelerator.CalculateInUpdate();
        
        Depth += Time.deltaTime * DrillAccelerator.CurrentSpeed;
        InteractCooldownService.OnUpdate();
    }

    public bool ThrowDiamondToFurnace()
    {
        if (GameStats.Diamonds <= 0)
            return false;
        
        if (GameSettings.FuelPerDiamond == GameSettings.MaxFuel)
            return false;
        
        Diamonds--;
        DrillAccelerator.NitroFuel += Mathf.Min(GameSettings.FuelPerDiamond, GameSettings.MaxFuel);
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

        _playerStartPos = Player.transform.position.y;
    }

    public void CollectDiamonds(float count)
    {
        Diamonds += count;
    }
}
