using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerContent : MonoBehaviour
    {
        private HorizontalLayoutGroup _horizontalLayoutGroup;
        private WeaponCaseOpenerRoot[] _weaponCaseOpenerRoots;
        
        private void Awake()
        {
            _horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
            _weaponCaseOpenerRoots = GetComponentsInChildren<WeaponCaseOpenerRoot>();
        }

        public void EnableLayout()
        {
            _horizontalLayoutGroup.enabled = true;
        }

        public void DisableLayout()
        {
            _horizontalLayoutGroup.enabled = false;
        }

        public void DisableWeapons()
        {
            foreach (WeaponCaseOpenerRoot weapon in _weaponCaseOpenerRoots)
                weapon.Disable();
        }

        public void EnableWeapons()
        {
            foreach (WeaponCaseOpenerRoot weapon in _weaponCaseOpenerRoots)
                weapon.Enable();
        }

        public WeaponCaseOpenerRoot[] GetWeapons() => _weaponCaseOpenerRoots;
    }
}