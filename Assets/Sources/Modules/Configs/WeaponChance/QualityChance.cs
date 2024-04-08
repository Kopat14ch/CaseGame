using Sources.Modules.Weapon.Enums;
using UnityEngine;

namespace Sources.Modules.Configs.WeaponChance
{
    [System.Serializable]
    public struct QualityChance
    {
        [field: SerializeField] public Quality Quality { get; private set; }
        [field: SerializeField, Range(0, 100)] public float Chance { get; private set; }
    }
}