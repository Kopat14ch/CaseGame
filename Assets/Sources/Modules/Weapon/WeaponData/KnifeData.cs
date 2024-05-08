using Sources.Modules.Weapon.Enums;
using UnityEngine;

namespace Sources.Modules.Weapon.WeaponData
{
    [CreateAssetMenu(fileName = "KnifeData", menuName = "BaseWeaponData/NewKnifeData", order = 51)]
    public class KnifeData : BaseWeaponData
    {
        [SerializeField] private KnifeName _knifeName;

        public override string GetName()
        {
            Name = _knifeName.ToString();
            return Name;
        }
    }
}