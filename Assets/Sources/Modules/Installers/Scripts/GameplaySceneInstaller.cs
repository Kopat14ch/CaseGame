using Sources.Modules.CaseOpener.Scripts;
using Sources.Modules.Common.Interfaces;
using Sources.Modules.Configs.WeaponChance;
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
        [SerializeField] private Content _contentCaseOpener;
        
        public override void InstallBindings()
        {
            InstallConfigs();
            InstallCaseOpener();
        }

        private void InstallConfigs()
        {
            Container.Bind<WeaponChanceConfig>().FromInstance(_weaponChanceConfig).AsSingle();
        }

        private void InstallCaseOpener()
        {
            Container.Bind<Content>().FromInstance(_contentCaseOpener).AsSingle();
            
            CaseOpenerFactory factoryInstance = new CaseOpenerFactory(_weaponCaseOpenerPrefab, _contentCaseOpener, Container);
            Container.Bind<CaseOpenerFactory>().FromInstance(factoryInstance).AsSingle().NonLazy();
            
            Container.Bind<CaseOpenerHandler>().AsSingle();
            Container.Bind<ISpeedUpdater>().To<CaseOpenerHandler>().FromResolve();
            
            factoryInstance.Inject();
            
            
            Container.Bind<CaseOpenerRoot>().FromInstance(_caseOpenerRoot).AsSingle();
            Container.Bind<WeaponCaseOpenerRoot>().FromInstance(_weaponCaseOpenerPrefab).AsSingle();
        }
    }
}