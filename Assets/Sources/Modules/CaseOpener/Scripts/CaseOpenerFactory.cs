using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerFactory
    {
        private const int WeaponCount = 70;
        
        private readonly WeaponCaseOpenerRoot _prefab;
        private readonly DiContainer _container;
        private readonly Transform _content;
        private readonly WeaponCaseOpenerRoot[] _weaponRoots;

        public CaseOpenerFactory(WeaponCaseOpenerRoot prefab, Content content, DiContainer container)
        {
            _prefab = prefab;
            _container = container; 
            _content = content.transform;
            _weaponRoots = new WeaponCaseOpenerRoot[WeaponCount];
            Create();
        }

        public void Inject()
        {
            foreach (var weaponCaseOpenerRoot in _weaponRoots)
                _container.Inject(weaponCaseOpenerRoot);
        }

        private void Create()
        {
            for (int i = 0; i < WeaponCount; i++)
                _weaponRoots[i] = Object.Instantiate(_prefab, _content);
            
            _container.Bind<WeaponCaseOpenerRoot[]>().FromInstance(_weaponRoots).AsSingle().NonLazy();
        }
    }
}