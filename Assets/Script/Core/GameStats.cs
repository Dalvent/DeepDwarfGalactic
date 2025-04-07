using System;
using UnityEngine.Serialization;

namespace Script
{
    [Serializable]
    public class GameStats
    {
        public float Depth;
        public float DrillFuel;
        public float Diamonds;

        public float MinSpeed;
        public float MaxSpeed;
        public float SpeedLoseFactor;
        public float SpeedLinerAcceleration;
        public float DecayExponent;
    }
}