using Sources.Modules.Configs.WeaponChance;
using Sources.Modules.Inventory.Scripts;
using Sources.Modules.Level.Configs;
using Sources.Modules.Level.Scripts;
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
        
        public override void InstallBindings()
        { 
            BindConfigs();
            BindWeaponRoot();
            BindInventory();
            BindWallet();
            BindLevel();
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
    }
}