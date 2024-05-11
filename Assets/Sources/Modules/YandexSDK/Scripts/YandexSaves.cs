using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Sources.Modules.Inventory.Interfaces;
using Sources.Modules.Level.Interfaces;
using Sources.Modules.Settings.Interfaces;
using Sources.Modules.Settings.Scripts.Sound;
using Sources.Modules.Wallet.Interfaces;
using Sources.Modules.Weapon.Scripts;
using Sources.Modules.Weapon.Scripts.WeaponData;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexSaves : IDisposable
    {
        private readonly ILevelHandlerEvent _levelHandlerEvent;
        private readonly IInventoryHandler _inventoryHandler;
        private readonly IWalletHandler _walletHandler;
        private readonly ISettingsRoot _settingsRoot;
        private YandexData _yandexData;

        public static YandexSaves Instance { get; private set; }
        public bool IsLoaded { get; private set; }

        public YandexSaves(ILevelHandlerEvent levelHandlerEvent, IInventoryHandler inventoryHandler, IWalletHandler walletHandler, ISettingsRoot settingsRoot)
        {
            Instance = this;
            
            _levelHandlerEvent = levelHandlerEvent;
            _inventoryHandler = inventoryHandler;
            _walletHandler = walletHandler;
            _settingsRoot = settingsRoot;

#if UNITY_EDITOR == false
            PlayerAccount.GetCloudSaveData(json =>
                {
                    _yandexData = JsonUtility.FromJson<YandexData>(json) ?? new YandexData();
                    IsLoaded = true;
                }
            );
#else
            _yandexData = new YandexData();
            IsLoaded = true;
#endif
            
            _levelHandlerEvent.LevelLimitUpdated += OnLevelLimitUpdated;
            _levelHandlerEvent.ExperienceUpdated += OnExperienceUpdated;
            _inventoryHandler.WeaponSold += OnWeaponSold;
            _inventoryHandler.WeaponAdded += OnWeaponAdded;
            _walletHandler.MoneyChanged += OnMoneyChanged;
            _settingsRoot.Disabled += OnSettingsDisabled;
        }

        public YandexData Load() => _yandexData;
        
        public void Dispose()
        {
            _levelHandlerEvent.LevelLimitUpdated -= OnLevelLimitUpdated;
            _levelHandlerEvent.ExperienceUpdated -= OnExperienceUpdated;
            _inventoryHandler.WeaponSold -= OnWeaponSold;
            _inventoryHandler.WeaponAdded -= OnWeaponAdded;
            _walletHandler.MoneyChanged -= OnMoneyChanged;
            _settingsRoot.Disabled -= OnSettingsDisabled;
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
            List<WeaponSaveData> newWeaponsData = new List<WeaponSaveData>(_yandexData.WeaponsData);
            
            WeaponSaveData weaponSaveData = new WeaponSaveData()
            {
                PathToFile = weaponRoot.Data.PathToFile,
                Price = weaponRoot.Price
            };

            bool canAdd;
            
            do
            {
                canAdd = true;
                
                foreach (var weapon in _yandexData.WeaponsData)
                {
                    if (weapon.Id == weaponRoot.Id)
                    {
                        canAdd = false;
                        break;
                    }
                }

                if (canAdd == false)
                    weaponRoot.UpdateId();

            } while (canAdd == false);

            weaponSaveData.Id = weaponRoot.Id;
            
            newWeaponsData.Add(weaponSaveData);
            
            _yandexData.WeaponsData = newWeaponsData.ToArray();
            
            Save();
        }

        private void OnWeaponSold(WeaponRoot weaponRoot)
        {
            var newWeapons = new List<WeaponSaveData>(_yandexData.WeaponsData);
            
            foreach (var weapon in _yandexData.WeaponsData)
            {
                if (weapon.Id != weaponRoot.Id)
                    continue;
                
                newWeapons.Remove(weapon);
            }
            
            _yandexData.WeaponsData = newWeapons.ToArray();
            
            Save();
        }

        private void OnMoneyChanged(float money)
        {
            _yandexData.Money = money;
            
            Save();
        }

        private void OnSettingsDisabled(SoundSettingsHandler soundSettingsHandler)
        {
            _yandexData.SoundData.LastVolume = soundSettingsHandler.LastVolume;
            _yandexData.SoundData.IsEnable = soundSettingsHandler.IsEnable;
            
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