using System.Collections.Generic;
using Sources.Modules.CaseOpener.Scripts;
using Sources.Modules.Common.Interfaces;
using Sources.Modules.Configs.WeaponChance;
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
            Container.Bind<WeaponCaseOpenerRoot>().FromInstance(_weaponCaseOpenerPrefab).AsSingle();
            Container.Bind<CaseOpenerFactory>().AsSingle().NonLazy();
            Container.Bind<CaseOpenerHandler>().AsSingle();
            Container.Bind<ISpeedUpdater>().To<CaseOpenerHandler>().FromResolve();
            Container.Bind<CaseOpenerRoot>().FromInstance(_caseOpenerRoot).AsSingle();
        }
    }
}