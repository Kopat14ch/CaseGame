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
            _weaponRoots = Create();
        }

        public WeaponCaseOpenerRoot[] GetWeapons() => _weaponRoots;

        public WeaponCaseOpenerRoot[] Create()
        {
            WeaponCaseOpenerRoot[] weaponRoots = new WeaponCaseOpenerRoot[WeaponCount];
            
            Debug.Log($"INIT {_content == null}");

            for (int i = 0; i < WeaponCount; i++)
            {
                weaponRoots[i] = _container.InstantiatePrefabForComponent<WeaponCaseOpenerRoot>(_prefab, _content);
            }

            return weaponRoots;
        }
    }
}