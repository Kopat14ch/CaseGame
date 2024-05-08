using Sources.Modules.Weapon.Enums;
using UnityEngine;

namespace Sources.Modules.Weapon.Scripts.WeaponData
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "BaseWeaponData/NewWeaponData", order = 51)]
    public class WeaponData : BaseWeaponData
    {
        [SerializeField] private WeaponName _weaponName;

        public override string GetName()
        {
            return _weaponName.ToString();
        }
    }
}