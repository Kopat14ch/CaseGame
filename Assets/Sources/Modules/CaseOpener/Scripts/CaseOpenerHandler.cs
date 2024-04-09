using System;
using System.Linq;
using DG.Tweening;
using Sources.Modules.Configs.WeaponChance;
using Sources.Modules.Weapon.Enums;
using Sources.Modules.Weapon.WeaponData;
using Sources.Modules.WeaponCaseOpener.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerHandler
    {
        private readonly WeaponCaseOpenerRoot[] _weaponCaseOpenerRoots;
        private readonly WeaponChanceConfig _weaponChanceConfig;

        public CaseOpenerHandler(WeaponCaseOpenerRoot[] caseOpenerRoots, WeaponChanceConfig weaponChanceConfig)
        {
            _weaponCaseOpenerRoots = caseOpenerRoots;
            _weaponChanceConfig = weaponChanceConfig;
        }

        public void Open(WeaponData[] weaponDatas, CaseOpenerArrow caseOpenerArrow, Transform transform)
        {
            foreach (var weaponCaseOpenerRoot in _weaponCaseOpenerRoots)
            {
                Quality quality = _weaponChanceConfig.GetQualityWithRandom(weaponDatas);
                
                WeaponData[] weaponDatasWithQuality = weaponDatas.Where(w => w.Quality == quality).ToArray();
                
                weaponCaseOpenerRoot.Init(weaponDatasWithQuality[Random.Range(0, weaponDatasWithQuality.Length)]);
            }

            Scrolling(caseOpenerArrow, transform);
        }

        private void Scrolling(CaseOpenerArrow caseOpenerArrow, Transform transform)
        {
            transform.DOMove(
                new Vector3(
                    caseOpenerArrow.transform.position.x -
                    _weaponCaseOpenerRoots[_weaponCaseOpenerRoots.Length - 6].transform.position.x -
                    Random.Range(0, 300), transform.position.y, transform.position.z), 10);
        }
    }
}