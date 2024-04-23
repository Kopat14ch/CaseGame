using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class DisableWeapons : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out WeaponCaseOpenerRoot weaponCaseOpenerRoot))
                weaponCaseOpenerRoot.Disable();
        }
    }
}