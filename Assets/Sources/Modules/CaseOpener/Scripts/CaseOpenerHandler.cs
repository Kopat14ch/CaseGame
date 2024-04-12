using System;
using System.Linq;
using DG.Tweening;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Configs.WeaponChance;
using Sources.Modules.Weapon.Enums;
using Sources.Modules.Weapon.WeaponData;
using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerHandler : ICaseOpenerHandler
    {
        private readonly WeaponChanceConfig _weaponChanceConfig;
        private CaseOpenerContent _caseOpenerRoots;

        private const float Offset = 100;
        private const int MinWeaponCaseOpenerIndex = 10;
        private const int MaxWeaponCaseOpenerIndex = 20;

        public event Action<Common.Scripts.Weapon> ScrollComplete;

        public CaseOpenerHandler(CaseOpenerContent caseOpenerRoots, WeaponChanceConfig weaponChanceConfig)
        {
            _caseOpenerRoots = caseOpenerRoots;
            _weaponChanceConfig = weaponChanceConfig;
        }

        public void Open(WeaponData[] weaponDatas, CaseOpenerArrow caseOpenerArrow, Transform transform)
        {
            WeaponCaseOpenerRoot[] weaponCaseOpenerRoots = _caseOpenerRoots.GetWeapons();
            
            foreach (var weaponCaseOpenerRoot in weaponCaseOpenerRoots)
            {
                WeaponQuality weaponQuality = _weaponChanceConfig.GetQualityWithRandom(weaponDatas);
                
                WeaponData[] weaponDatasWithQuality = weaponDatas.Where(w => w.Quality == weaponQuality).ToArray();
                
                weaponCaseOpenerRoot.Init(weaponDatasWithQuality[Random.Range(0, weaponDatasWithQuality.Length)]);
            }

            Scrolling(caseOpenerArrow, transform, weaponCaseOpenerRoots);
        }

        private void Scrolling(CaseOpenerArrow caseOpenerArrow, Transform transform, WeaponCaseOpenerRoot[] weaponCaseOpenerRoot)
        {
            int weaponCaseOpenerIndex = weaponCaseOpenerRoot.Length - Random.Range(MinWeaponCaseOpenerIndex, MaxWeaponCaseOpenerIndex);
            WeaponCaseOpenerRoot tempCaseOpenerRoot = weaponCaseOpenerRoot[weaponCaseOpenerIndex];
            
            Tween localMoveX = transform.DOLocalMoveX(caseOpenerArrow.transform.localPosition.x - 
                                                      tempCaseOpenerRoot.transform.localPosition.x - Random.Range(-Offset, Offset), 10);
            
            localMoveX.OnComplete(() => ScrollComplete?.Invoke(tempCaseOpenerRoot));
        }
    }
}