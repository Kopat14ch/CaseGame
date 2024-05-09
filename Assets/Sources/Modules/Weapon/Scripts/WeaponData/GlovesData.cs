﻿using Sources.Modules.Weapon.Enums;
using UnityEngine;

namespace Sources.Modules.Weapon.Scripts.WeaponData
{
    
    [CreateAssetMenu(fileName = "GlovesData", menuName = "BaseWeaponData/NewGlovesData", order = 51)]
    public class GlovesData : BaseWeaponData
    {
        [SerializeField] private GlovesName _glovesName;

        public override string GetName()
        {
            return _glovesName.ToString();
        }
    }
}