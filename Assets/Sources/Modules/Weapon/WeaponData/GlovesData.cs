using Sources.Modules.Weapon.Enums;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Weapon.WeaponData
{
    
    [CreateAssetMenu(fileName = "GlovesData", menuName = "BaseWeaponData/NewGlovesData", order = 51)]
    public class GlovesData : BaseWeaponData
    {
        [SerializeField] private GlovesName _glovesName;

        public override string GetName()
        {
            Name = _glovesName.ToString();
            return Name;
        }
    }
}