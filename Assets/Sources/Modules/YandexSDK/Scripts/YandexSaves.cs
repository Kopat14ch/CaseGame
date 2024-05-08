using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Sources.Modules.Inventory.Interfaces;
using Sources.Modules.Level.Interfaces;
using Sources.Modules.Wallet.Interfaces;
using Sources.Modules.Weapon.Scripts;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexSaves : IDisposable
    {
        private readonly ILevelHandlerEvent _levelHandlerEvent;
        private readonly IInventoryHandler _inventoryHandler;
        private readonly IWalletHandler _walletHandler;
        private YandexData _yandexData;

        public static YandexSaves Instance { get; private set; }

        public YandexSaves(ILevelHandlerEvent levelHandlerEvent, IInventoryHandler inventoryHandler, IWalletHandler walletHandler)
        {
            Instance = this;
            
            _levelHandlerEvent = levelHandlerEvent;
            _inventoryHandler = inventoryHandler;
            _walletHandler = walletHandler;

#if UNITY_EDITOR == false
            PlayerAccount.GetCloudSaveData(json =>
                _yandexData = JsonUtility.FromJson<YandexData>(json) ?? new YandexData()
            );
#else
            _yandexData = new YandexData();
#endif
            
            _levelHandlerEvent.LevelLimitUpdated += OnLevelLimitUpdated;
            _levelHandlerEvent.ExperienceUpdated += OnExperienceUpdated;
            _inventoryHandler.WeaponSold += OnWeaponSold;
            _inventoryHandler.WeaponAdded += OnWeaponAdded;
            _walletHandler.MoneyChanged += OnMoneyChanged;
        }

        public YandexData Load() => _yandexData;
        
        public void Dispose()
        {
            _levelHandlerEvent.LevelLimitUpdated -= OnLevelLimitUpdated;
            _levelHandlerEvent.ExperienceUpdated -= OnExperienceUpdated;
            _inventoryHandler.WeaponSold -= OnWeaponSold;
            _inventoryHandler.WeaponAdded -= OnWeaponAdded;
            _walletHandler.MoneyChanged -= OnMoneyChanged;
        }
        
        private void OnLevelLimitUpdated(int level, uint maxExperience)
        {
            _yandexData.Level = level;
            _yandexData.MaxExperience = maxExperience;

            Save();
        }

        private void OnExperienceUpdated(uint experience)
        {
            _yandexData.Experience = experience;
            
            Save();
        }

        private void OnWeaponAdded(WeaponRoot weaponRoot)
        {
            var newWeapons = new List<BaseWeaponData>(_yandexData.WeaponsData) { weaponRoot.Data };
            _yandexData.WeaponsData = newWeapons.ToArray();
            
            Debug.Log(JsonUtility.ToJson(weaponRoot));
            
            Save();
        }

        private void OnWeaponSold(WeaponRoot weaponRoot)
        {
            var newWeapons = new List<BaseWeaponData>(_yandexData.WeaponsData);
            newWeapons.Remove(weaponRoot.Data);
            _yandexData.WeaponsData = newWeapons.ToArray();
            
            Save();
        }

        private void OnMoneyChanged(float money)
        {
            _yandexData.Money = money;
            
            Save();
        }

        private void Save()
        {
#if UNITY_EDITOR == false
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(_yandexData));
#endif
        }
    }
}