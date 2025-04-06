using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class CooldownInfo
    {
        public float TimeLeft { get; set; }
        public float TimeRequested { get; set; }
    }
    
    public class InteractCooldownService
    {
        private readonly Dictionary<string, CooldownInfo> _cooldowns = new();
        private List<string> _cooldownToDelete = new();

        public void MakeCooldown(string key, float value)
        {
            _cooldowns[key] = new CooldownInfo() { TimeLeft = value, TimeRequested = value };
        }

        public float GetPassedPresent(string key)
        {
            if (!_cooldowns.TryGetValue(key, out CooldownInfo cooldownInfo))
                return 1;

            return 1 - cooldownInfo.TimeLeft / cooldownInfo.TimeRequested;
        }

        public void OnUpdate()
        {
            if (_cooldowns.Count == 0)
                return;

            foreach (var cooldown in _cooldowns)
            {
                cooldown.Value.TimeLeft -= Time.deltaTime;
                
                if (cooldown.Value.TimeLeft <= 0)
                    _cooldownToDelete.Add(cooldown.Key);
            }

            foreach (var cooldownToDelete in _cooldownToDelete)
                _cooldowns.Remove(cooldownToDelete);
            
            _cooldownToDelete.Clear();
        }
    }
}