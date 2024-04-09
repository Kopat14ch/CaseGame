using Sources.Modules.CaseOpener.Scripts;
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
        [SerializeField] private int _weaponsInCaseOpener;
        
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
            Container.Bind<WeaponCaseOpenerRoot[]>().FromInstance(new WeaponCaseOpenerRoot[_weaponsInCaseOpener]).AsSingle().NonLazy();
            
            Container.Bind<CaseOpenerHandler>().AsSingle();
            Container.BindInterfacesTo<CaseOpenerHandler>().FromResolve();
            
            Container.Bind<CaseOpenerRoot>().FromInstance(_caseOpenerRoot).AsSingle();
            Container.BindInterfacesTo<CaseOpenerRoot>().FromResolve();
            
            Container.Bind<WeaponCaseOpenerRoot>().FromInstance(_weaponCaseOpenerPrefab).AsSingle();
            
            Container.Bind<CaseOpenerFactory>().AsSingle().NonLazy();
        }
    }
}