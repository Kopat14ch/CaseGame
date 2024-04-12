using Sources.Modules.CaseOpener.Scripts;
using Sources.Modules.Configs.WeaponChance;
using Sources.Modules.Inventory.Scripts;
using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Installers.Scripts
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private WeaponChanceConfig _weaponChanceConfig;
        [SerializeField] private CaseOpenerRoot _caseOpenerRoot;
        [SerializeField] private WeaponCaseOpenerRoot _weaponCaseOpenerPrefab;
        [SerializeField] private CaseOpenerContent _contentCaseOpener;
        [SerializeField] private InventoryContent _inventoryContent;
        
        public override void InstallBindings()
        {
            BindConfigs();
            BindCaseOpener();
            BindInventory();
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
        }

        private void BindInventory()
        {
            Container.Bind<InventoryContent>().FromInstance(_inventoryContent).AsSingle().NonLazy();
            Container.Bind<InventoryFactory>().AsCached().NonLazy();
            Container.Bind<InventoryHandler>().AsSingle().NonLazy();
        }
    }
}