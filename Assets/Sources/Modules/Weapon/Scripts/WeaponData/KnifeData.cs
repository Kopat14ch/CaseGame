using Sources.Modules.Weapon.Enums;
using UnityEngine;

namespace Sources.Modules.Weapon.Scripts.WeaponData
{
    [CreateAssetMenu(fileName = "KnifeData", menuName = "BaseWeaponData/NewKnifeData", order = 51)]
    public class KnifeData : BaseWeaponData
    {
        [SerializeField] private KnifeName _knifeName;

        public override string GetName()
        {
            return _knifeName.ToString();
        }
    }
}