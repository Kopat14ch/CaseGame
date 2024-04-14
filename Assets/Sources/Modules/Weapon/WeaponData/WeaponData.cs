using Sources.Modules.Weapon.Enums;
using UnityEngine;

namespace Sources.Modules.Weapon.WeaponData
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "BaseWeaponData/NewWeaponData", order = 51)]
    public class WeaponData : BaseWeaponData
    {
        [SerializeField] private WeaponName _weaponName;

        public override string GetName() => _weaponName.ToString();
    }
}