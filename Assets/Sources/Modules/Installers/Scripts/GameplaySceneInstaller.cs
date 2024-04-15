using Sources.Modules.CaseOpener.Scripts;
using Sources.Modules.Configs.WeaponChance;
using Sources.Modules.Inventory.Scripts;
using Sources.Modules.Level.Configs;
using Sources.Modules.Level.Scripts;
using Sources.Modules.MiniGames.Clicker.Scripts;
using Sources.Modules.Wallet.Scripts;
using Sources.Modules.Weapon.Scripts;
using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Sources.Modules.Installers.Scripts
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private WeaponChanceConfig _weaponChanceConfig;
        [SerializeField] private CaseOpenerRoot _caseOpenerRoot;
        [SerializeField] private WeaponCaseOpenerRoot _weaponCaseOpenerPrefab;
        [SerializeField] private WeaponRoot _weaponRootPrefab;
        [SerializeField] private CaseOpenerContent _contentCaseOpener;
        [SerializeField] private CaseOpenerView _caseOpenerView;
        [SerializeField] private InventoryContent _inventoryContent;
        [SerializeField] private WalletRoot _walletRoot;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private LevelConfig _levelConfig;
        [FormerlySerializedAs("_coin")] [SerializeField] private CoinRoot coinRoot;
        
        public override void InstallBindings()
        {
            BindConfigs();
            BindWeaponRoot();
            BindCaseOpener();
            BindInventory();
            BindWallet();
            BindLevel();
            BindClicker();
        }

        private void BindConfigs()
        {
            Container.Bind<WeaponChanceConfig>().FromInstance(_weaponChanceConfig).AsSingle();
        }

        private void BindCaseOpener()
        {
            Container.Bind<CaseOpenerContent>().FromInstance(_contentCaseOpener).AsSingle().NonLazy();
            
            Container.Bind<CaseOpenerHandler>().AsSingle();
            Container.BindInterfacesTo<CaseOpenerHandler>().FromResolve();
            
            Container.Bind<CaseOpenerRoot>().FromInstance(_caseOpenerRoot).AsSingle();
            Container.BindInterfacesTo<CaseOpenerRoot>().FromResolve();
            
            Container.Bind<WeaponCaseOpenerRoot>().FromInstance(_weaponCaseOpenerPrefab).AsSingle();
            Container.Bind<CaseOpenerFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CaseOpenerView>().FromInstance(_caseOpenerView).AsSingle();
        }

        private void BindInventory()
        {
            Container.BindInterfacesTo<InventoryView>().FromInstance(_inventoryView).AsSingle();
            Container.Bind<InventoryContent>().FromInstance(_inventoryContent).AsSingle().NonLazy();
            Container.Bind<InventoryFactory>().AsCached().NonLazy();
            Container.Bind<InventoryHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InventoryHandler>().FromResolve();
        }

        private void BindWeaponRoot()
        {
            Container.Bind<WeaponRoot>().FromInstance(_weaponRootPrefab).AsSingle();
        }

        private void BindWallet()
        {
            Container.BindInterfacesTo<WalletRoot>().FromInstance(_walletRoot).AsSingle();
        }

        private void BindLevel()
        {
            Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();
            Container.Bind<LevelHandler>().AsSingle();
            Container.BindInterfacesTo<LevelHandler>().FromResolve();
        }

        private void BindClicker()
        {
            Container.Bind<CoinRoot>().FromInstance(coinRoot).AsSingle();
            Container.Bind<ClickerHandler>().AsSingle().NonLazy();
        }
    }
}