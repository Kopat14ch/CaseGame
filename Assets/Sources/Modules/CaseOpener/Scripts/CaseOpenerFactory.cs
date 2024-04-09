using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerFactory
    {
        private readonly WeaponCaseOpenerRoot _prefab;
        private readonly DiContainer _container;
        private readonly Transform _content;
        private readonly WeaponCaseOpenerRoot[] _weaponRoots;

        public CaseOpenerFactory(WeaponCaseOpenerRoot prefab, WeaponCaseOpenerRoot[] weaponRoots, Content content, DiContainer container)
        {
            _prefab = prefab;
            _container = container; 
            _content = content.transform;
            _weaponRoots = weaponRoots;
            Create();
        }
        
        private void Create()
        {
            for (int i = 0; i < _weaponRoots.Length; i++)
                _weaponRoots[i] = _container.InstantiatePrefabForComponent<WeaponCaseOpenerRoot>(_prefab, _content);
        }
    }
}