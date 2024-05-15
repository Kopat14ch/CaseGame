using Sources.Modules.Configs.WeaponChance;
using Sources.Modules.Inventory.Scripts;
using Sources.Modules.Level.Configs;
using Sources.Modules.Level.Scripts;
using Sources.Modules.Settings.Scripts;
using Sources.Modules.Settings.Scripts.Sound;
using Sources.Modules.Wallet.Scripts;
using Sources.Modules.Weapon.Scripts;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Installers.Scripts
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private WeaponChanceConfig _weaponChanceConfig;
        [SerializeField] private WeaponRoot _weaponRootPrefab;
        [SerializeField] private InventoryContent _inventoryContent;
        [SerializeField] private WalletRoot _walletRoot;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private SettingsRoot _settingsRoot;
        
        public override void InstallBindings()
        { 
            BindConfigs();
            BindWeaponRoot();
            BindInventory();
            BindWallet();
            BindLevel();
            BindSettings();
        }

        private void BindConfigs()
        {
            Container.Bind<WeaponChanceConfig>().FromInstance(_weaponChanceConfig).AsSingle();
            Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();
        }

        private void BindInventory()
        {
            Container.BindInterfacesTo<InventoryView>().FromInstance(_inventoryView).AsSingle();
            Container.Bind<InventoryContent>().FromInstance(_inventoryContent).AsSingle().NonLazy();
            Container.Bind<InventoryFactory>().AsSingle().NonLazy();
            Container.Bind<InventoryHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InventoryHandler>().FromResolve().NonLazy();
        }

        private void BindWeaponRoot()
        {
            Container.Bind<WeaponRoot>().FromInstance(_weaponRootPrefab).AsSingle();
        }

        private void BindWallet()
        {
            Container.Bind<WalletHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WalletHandler>().FromResolve();
            Container.BindInterfacesTo<WalletRoot>().FromInstance(_walletRoot).AsSingle();
        }

        private void BindLevel()
        {
            Container.Bind<LevelHandler>().AsSingle();
            Container.BindInterfacesTo<LevelHandler>().FromResolve();
        }

        private void BindSettings()
        {
            Container.BindInterfacesTo<SettingsRoot>().FromInstance(_settingsRoot).AsSingle().NonLazy();

            SoundSettingsHandler soundSettingsHandler = new SoundSettingsHandler(_settingsRoot.SoundRoot.Slider,
                _settingsRoot.SoundRoot.ToggleButton,
                _settingsRoot.SoundRoot.Image,
                _settingsRoot.SoundRoot.EnableSprite,
                _settingsRoot.SoundRoot.DisableSprite);
            Container.Bind<SoundSettingsHandler>().FromInstance(soundSettingsHandler).AsSingle().NonLazy();
            Container.BindInterfacesTo<SoundSettingsHandler>().FromResolve();

        }
    }
}