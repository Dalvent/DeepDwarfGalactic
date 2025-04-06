using System.Collections.Generic;
using Script.Interact;
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

        public bool CanInteract(string key)
        {
            return GetPassedPresent(key) >= 1;
        }

        public void MakeCooldown(InteractCooldownInfo cooldownInfo)
        {
            _cooldowns[cooldownInfo.CooldownGroupKey] = new CooldownInfo() 
            {
                TimeLeft = cooldownInfo.CooldownTime,
                TimeRequested = cooldownInfo.CooldownTime 
            };
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

                Debug.Log($"{cooldown.Key} {cooldown.Value.TimeLeft}");
            }

            foreach (var cooldownToDelete in _cooldownToDelete)
                _cooldowns.Remove(cooldownToDelete);
            
            _cooldownToDelete.Clear();
        }
    }
}