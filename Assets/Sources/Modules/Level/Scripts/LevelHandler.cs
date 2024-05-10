using System;
using Cysharp.Threading.Tasks;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Inventory.Interfaces;
using Sources.Modules.Level.Configs;
using Sources.Modules.Level.Interfaces;
using Sources.Modules.Weapon.Enums;
using Sources.Modules.Weapon.Scripts;
using Sources.Modules.YandexSDK.Scripts;

namespace Sources.Modules.Level.Scripts
{
    public class LevelHandler : ILevelHandlerEvent, IDisposable
    {
        private readonly LevelConfig _config;
        private readonly ICaseOpenerHandler _caseOpenerHandler;
        private readonly IInventoryHandler _inventoryHandler;

        private int _current;
        private uint _experience;
        private uint _maxExperience;
        
        public event Action<uint> ExperienceUpdated;
        public event Action<int, uint> LevelLimitUpdated;
        public event Action<int> LevelUpdated; 

        public LevelHandler(LevelConfig config, ICaseOpenerHandler caseOpenerHandler, IInventoryHandler inventoryHandler)
        {
            _config = config;
            _caseOpenerHandler = caseOpenerHandler;
            _inventoryHandler = inventoryHandler;

            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
            _inventoryHandler.WeaponSold += OnWeaponSold;
        }
        
        public async void Init()
        {
            await UniTask.WaitUntil(() => YandexSaves.Instance.IsLoaded);

            YandexData yandexSaves = YandexSaves.Instance.Load();
            
            _current = yandexSaves.Level;
            _experience = yandexSaves.Experience;
            _maxExperience = yandexSaves.MaxExperience;
            
            LevelLimitUpdated?.Invoke(_current, _maxExperience);
            LevelUpdated?.Invoke(_current);
            ExperienceUpdated?.Invoke(_experience);
        }

        public void Dispose()
        {
            _caseOpenerHandler.ScrollComplete -= OnScrollComplete;
            _inventoryHandler.WeaponSold -= OnWeaponSold;
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

        private void OnWeaponSold(WeaponRoot weaponRoot)
        {
            AddExperience(_config.GetExperienceWithQuality(weaponRoot.Data.Quality) / 2);
        }

        private void TryUp()
        {
            if (_experience < _maxExperience) 
                return;
            
            _experience -= _maxExperience;
            _maxExperience = (uint)(_maxExperience * _config.LimitMultiplier);
            _current++;
            
            LevelLimitUpdated?.Invoke(_current, _maxExperience);
            LevelUpdated?.Invoke(_current);
        }
    }
}