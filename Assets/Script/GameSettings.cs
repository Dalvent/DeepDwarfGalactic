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
        
        [Header("Depth")]
        public float StartDepth = 50f;
        public float MaxDepth = 1000f;
        
        [Header("Depth")]
        public float MinSpeed = 1f;
        public float MaxSpeed = 100f;
        public float SpeedLinerAcceleration = 1.0f;
        public float SpeedLoseFactor = 0.5f;
        public float DecayExponent = 1.2f;
        
        [Header("Dwarf")]
        public int StartHP = 3;
    }
}