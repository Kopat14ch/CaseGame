using System;
using Sources.Modules.Weapon.Scripts;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine.Serialization;

namespace Sources.Modules.YandexSDK.Scripts
{
    [Serializable]
    public class YandexData
    {
        [FormerlySerializedAs("Weapons")] public BaseWeaponData[] WeaponsData;
        public float Money;
        public int Level;
        public uint Experience;
        public uint MaxExperience;
        
        public YandexData()
        {
            WeaponsData = Array.Empty<BaseWeaponData>();
        }
    }
}