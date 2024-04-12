using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerFactory
    {
        private const int WeaponCount = 100;
        
        private readonly WeaponCaseOpenerRoot _prefab; 
        private readonly Transform _content;
        private readonly WeaponCaseOpenerRoot[] _weaponRoots;

        public CaseOpenerFactory(WeaponCaseOpenerRoot prefab, CaseOpenerContent content)
        {
            _prefab = prefab;
            _content = content.transform;
            _weaponRoots = new WeaponCaseOpenerRoot[WeaponCount];
            Create();
        }
        
        private void Create()
        {
            for (int i = 0; i < _weaponRoots.Length; i++)
                _weaponRoots[i] = Object.Instantiate(_prefab, _content);
        }
    }
}