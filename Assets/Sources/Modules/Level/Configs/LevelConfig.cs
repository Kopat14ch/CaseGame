using System.Collections.Generic;
using Sources.Modules.Weapon.Enums;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Sources.Modules.Level.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public uint StartLimit { get; private set; }
        [field: SerializeField] public float LimitMultiplier { get; private set; }
        
        private readonly Dictionary<WeaponQuality, Vector2> _experienceRanges = new()
        {
            { WeaponQuality.Common, new Vector2(1, 2) },
            { WeaponQuality.Uncommon, new Vector2(2, 2) },
            { WeaponQuality.Rare, new Vector2(2, 3) },
            { WeaponQuality.Mythical, new Vector2(7, 10) },
            { WeaponQuality.Legendary, new Vector2(15, 20) },
            { WeaponQuality.Ancient, new Vector2(25, 35) },
            { WeaponQuality.Immortal, new Vector2(40, 60) }
        };
        
        
        public uint GetExperienceWithQuality(WeaponQuality quality)
        {
            if (_experienceRanges.TryGetValue(quality, out Vector2 range))
                return (uint)Random.Range(range.X, range.Y + 1);
            
            return 0;
        }
    }
}