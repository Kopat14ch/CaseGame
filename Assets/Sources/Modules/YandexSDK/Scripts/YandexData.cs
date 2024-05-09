using System;
using Sources.Modules.Weapon.Scripts.WeaponData;

namespace Sources.Modules.YandexSDK.Scripts
{
    [Serializable]
    public class YandexData
    {
        public WeaponSaveData[] WeaponsData;
        public float Money;
        public int Level;
        public uint Experience;
        public uint MaxExperience;
        
        public YandexData()
        {
            WeaponsData = Array.Empty<WeaponSaveData>();
        }
    }
}