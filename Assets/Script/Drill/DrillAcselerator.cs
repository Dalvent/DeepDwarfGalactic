using System;
using UnityEngine;

namespace Script
{
    public class DrillAccelerator
    {
        private readonly GameStats _gameStats;
        private readonly GameSettings _gameSettings;
        private readonly GameUI _gameUI;

        public DrillAccelerator(GameStats gameStats, GameUI gameUI, GameSettings gameSettings)
        {
            _gameStats = gameStats;
            _gameUI = gameUI;
            _gameSettings = gameSettings;
            _nitroFuel = _gameStats.DrillFuel;
        }
        
        public bool UseNitro { get; set; }
        public float CurrentSpeed { private set; get; }

        private float _nitroFuel;
        public float NitroFuel
        {
            get => _nitroFuel;
            set
            {
                _nitroFuel = value;
                _gameUI.UpdateFuel(_nitroFuel);
            }
        }

        public void CalculateInUpdate()
        {
            if (UseNitro)
                CalculateFuel();

            CalculateSpeed();
        }

        private void CalculateSpeed()
        {
            if (UseNitro && NitroFuel > 0)
            {
                // Линейный прирост
                CurrentSpeed += _gameSettings.SpeedLinerAcceleration * Time.deltaTime;
            }
            else
            {
                float decay = _gameStats.SpeedLoseFactor * Mathf.Pow(CurrentSpeed, _gameSettings.DecayExponent);
                CurrentSpeed -= decay * Time.deltaTime;
            }

            CurrentSpeed = Mathf.Clamp(CurrentSpeed, _gameSettings.MinSpeed, _gameSettings.MaxSpeed);
        }

        private void CalculateFuel()
        {
            NitroFuel = Mathf.Max(NitroFuel - Time.deltaTime * _gameSettings.FuelConsumptionRateBySecond, 0);
        }
    }
}