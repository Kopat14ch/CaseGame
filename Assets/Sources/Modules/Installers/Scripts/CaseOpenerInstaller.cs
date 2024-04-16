using Sources.Modules.CaseOpener.Scripts;
using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Installers.Scripts
{
    public class CaseOpenerInstaller : MonoInstaller
    {
        [SerializeField] private CaseOpenerContent _contentCaseOpener;
        [SerializeField] private CaseOpenerRoot _caseOpenerRoot;
        [SerializeField] private WeaponCaseOpenerRoot _weaponCaseOpenerPrefab;
        [SerializeField] private CaseOpenerView _caseOpenerView;
        
        public override void InstallBindings()
        {
            BindCaseOpener();
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
    }
}