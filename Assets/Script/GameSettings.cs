using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Fuel")]
        public float MaxFuel = 100f;
        public float StartFuel = 50f;
        public float FuelConsumptionRateBySecond = 1f;
        
        [Header("Diamonds")]
        public int StartDiamond = 5;
        public float FuelPerDiamond = 10f;
        
        [Header("Dwarf")]
        public float StartDepth = 50f;
        
        [FormerlySerializedAs("StartLives")] [Header("Dwarf")]
        public int StartHP = 3;
    }
}