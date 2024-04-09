using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerArrow : MonoBehaviour
    {
        public WeaponCaseOpenerRoot CurrentWeaponCaseOpenerRoot { get; private set; }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out WeaponCaseOpenerRoot weaponCaseOpenerRoot))
                CurrentWeaponCaseOpenerRoot = weaponCaseOpenerRoot;
        }
    }
}