using System;
using System.Linq;
using DG.Tweening;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Configs.WeaponChance;
using Sources.Modules.Weapon.Enums;
using Sources.Modules.Weapon.Scripts.WeaponData;
using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerHandler : ICaseOpenerHandler
    {
        private readonly WeaponChanceConfig _weaponChanceConfig;
        private readonly CaseOpenerContent _caseOpenerRoots;

        private const float Offset = 100;
        private const int MinWeaponCaseOpenerIndex = 10;
        private const int MaxWeaponCaseOpenerIndex = 20;
        private const float OpenDuration = 9;

        public event Action<Common.Scripts.Weapon> ScrollComplete;

        public CaseOpenerHandler(CaseOpenerContent caseOpenerRoots, WeaponChanceConfig weaponChanceConfig)
        {
            _caseOpenerRoots = caseOpenerRoots;
            _weaponChanceConfig = weaponChanceConfig;
        }

        public void Open(BaseWeaponData[] weaponDatas, CaseOpenerArrow caseOpenerArrow, Transform transform)
        {
            WeaponCaseOpenerRoot[] weaponCaseOpenerRoots = _caseOpenerRoots.GetWeapons();
            
            foreach (var weaponCaseOpenerRoot in weaponCaseOpenerRoots)
            {
                WeaponQuality weaponQuality = _weaponChanceConfig.GetQualityWithRandom(weaponDatas);
                
                BaseWeaponData[] weaponDatasWithQuality = weaponDatas.Where(w => w.Quality == weaponQuality).ToArray();
                
                weaponCaseOpenerRoot.UpdateId();
                
                weaponCaseOpenerRoot.Init(weaponDatasWithQuality[Random.Range(0, weaponDatasWithQuality.Length)], weaponCaseOpenerRoot.Id);
            }

            Scrolling(caseOpenerArrow, transform, weaponCaseOpenerRoots);
        }

        private void Scrolling(CaseOpenerArrow caseOpenerArrow, Transform transform, WeaponCaseOpenerRoot[] weaponCaseOpenerRoot)
        {
            int weaponCaseOpenerIndex = weaponCaseOpenerRoot.Length - Random.Range(MinWeaponCaseOpenerIndex, MaxWeaponCaseOpenerIndex);
            WeaponCaseOpenerRoot tempCaseOpenerRoot = weaponCaseOpenerRoot[weaponCaseOpenerIndex];
            
            Tween localMoveX = transform.DOLocalMoveX(caseOpenerArrow.transform.localPosition.x - 
                                                      tempCaseOpenerRoot.transform.localPosition.x - Random.Range(-Offset, Offset), OpenDuration)
                .SetEase(Ease.OutCirc);
            
            localMoveX.OnComplete(() => ScrollComplete?.Invoke(tempCaseOpenerRoot));
        }

        private void OpenAgain()
        {
            
        }
    }
}