using System;
using Sources.Modules.Settings.Scripts.Sound;
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
        public SoundData SoundData;
        
        public YandexData()
        {
            Money = 1000;
            Experience = 0;
            MaxExperience = 50;
            WeaponsData = Array.Empty<WeaponSaveData>();
            SoundData = new SoundData();
        }
    }
}