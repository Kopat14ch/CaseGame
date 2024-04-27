using System;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Inventory.Interfaces;
using Sources.Modules.Level.Configs;
using Sources.Modules.Level.Interfaces;
using Sources.Modules.Weapon.Enums;

namespace Sources.Modules.Level.Scripts
{
    public class LevelHandler : ILevelHandlerEvent, IDisposable
    {
        private readonly LevelConfig _config;
        private readonly ICaseOpenerHandler _caseOpenerHandler;
        private readonly IInventoryHandler _inventoryHandler;

        private int _current;
        private uint _experience;
        private uint _limit;
        
        public event Action<uint> ExperienceUpdated;
        public event Action<int, uint> LevelLimitUpdated;
        public event Action<int> LevelUpdated; 

        public LevelHandler(LevelConfig config, ICaseOpenerHandler caseOpenerHandler, IInventoryHandler inventoryHandler)
        {
            _config = config;
            _caseOpenerHandler = caseOpenerHandler;
            _inventoryHandler = inventoryHandler;

            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
            _inventoryHandler.WeaponSelled += OnWeaponSelled;
        }
        
        public void Init()
        {
            _current = 0;
            _experience = 0;
            _limit = 50;
            
            LevelLimitUpdated?.Invoke(_current, _limit);
            LevelUpdated?.Invoke(_current);
            ExperienceUpdated?.Invoke(_experience);
        }

        public void Dispose()
        {
            _caseOpenerHandler.ScrollComplete -= OnScrollComplete;
        }
        
        private void AddExperience(uint exp)
        {
            _experience += exp;
            TryUp();
            
            ExperienceUpdated?.Invoke(_experience);
        }

        private void OnScrollComplete(Common.Scripts.Weapon weapon)
        {
            AddExperience(_config.GetExperienceWithQuality(weapon.Data.Quality));
        }

        private void OnWeaponSelled(WeaponQuality quality)
        {
            AddExperience(_config.GetExperienceWithQuality(quality) / 2);
        }

        private void TryUp()
        {
            if (_experience < _limit) 
                return;
            
            _experience -= _limit;
            _limit = (uint)(_limit * _config.LimitMultiplier);
            _current++;
            
            LevelLimitUpdated?.Invoke(_current, _limit);
            LevelUpdated?.Invoke(_current);
        }
    }
}