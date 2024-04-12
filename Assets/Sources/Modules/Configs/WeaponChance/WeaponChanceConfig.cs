using System.Collections.Generic;
using System.Linq;
using Sources.Modules.Weapon.Enums;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;

namespace Sources.Modules.Configs.WeaponChance
{
    [CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "Configs/WeaponChanceConfig")]
    public class WeaponChanceConfig : ScriptableObject
    {
        [SerializeField] private QualityChance[] _qualityChances;

        private const float MaxChance = 101;

        public WeaponQuality GetQualityWithRandom(WeaponData[] weaponDatas)
        {
            float chance = Random.Range(0, MaxChance);
            HashSet<WeaponQuality> qualities = new HashSet<WeaponQuality>(weaponDatas.Select(wd => wd.Quality));
            
            foreach (var quality in qualities.OrderByDescending(q => q))
            {
                QualityChance qualityChance = _qualityChances.FirstOrDefault(tempQuality => tempQuality.WeaponQuality == quality);
                
                if (chance <= qualityChance.Chance)
                    return quality;
            }

            return qualities.OrderBy(q => q).First();

        }
    }
}