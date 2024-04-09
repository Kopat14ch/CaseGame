using System;
using System.Linq;
using Sources.Modules.Common.Interfaces;
using Sources.Modules.Configs.WeaponChance;
using Sources.Modules.Weapon.Enums;
using Sources.Modules.Weapon.WeaponData;
using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerHandler : ISpeedUpdater
    {
        private readonly WeaponCaseOpenerRoot[] _weaponCaseOpenerRoots;
        private readonly WeaponChanceConfig _weaponChanceConfig;

        public event Action<float> SpeedUpdated; 

        public CaseOpenerHandler(WeaponCaseOpenerRoot[] caseOpenerRoots, WeaponChanceConfig weaponChanceConfig)
        {
            _weaponCaseOpenerRoots = caseOpenerRoots;
            _weaponChanceConfig = weaponChanceConfig;
        }

        public void Open(WeaponData[] weaponDatas)
        {
            foreach (var weaponCaseOpenerRoot in _weaponCaseOpenerRoots)
            {
                Quality quality = _weaponChanceConfig.GetQualityWithRandom(weaponDatas);
                
                WeaponData[] weaponDatasWithQuality = weaponDatas.Where(w => w.Quality == quality).ToArray();
                
                weaponCaseOpenerRoot.Init(weaponDatasWithQuality[Random.Range(0, weaponDatasWithQuality.Length)]);
            }
        }
        


        private void GetRandomWeapon()
        {
            
        }
    }
}