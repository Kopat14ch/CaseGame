using System;
using System.Collections.Generic;
using Sources.Modules.Weapon.Enums;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.Weapon.Scripts.WeaponData
{
    public abstract class BaseWeaponData : ScriptableObject
    {
        [field: SerializeField] public string SkinName { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public WeaponQuality Quality { get; private set; }
        [field: SerializeField] public string PathToFile { private set; get; }
        
        [SerializeField] private float _minPrice;
        [SerializeField] private float _maxPrice;

        private readonly Dictionary<WeaponQuality, Color> _colorWithQuality = new()
        {
            {WeaponQuality.Common, new Color(0.5f, 0.5f, 0.5f)},
            {WeaponQuality.Uncommon, new Color(0.678f, 0.847f, 0.902f)},
            {WeaponQuality.Rare, new Color(0f, 0f, 0.5f)},
            {WeaponQuality.Mythical, new Color(0.4f, 0f, 1f)},
            {WeaponQuality.Legendary, new Color(1f, 0f, 1f)},
            {WeaponQuality.Ancient, new Color(1f, 0f, 0f)},
            {WeaponQuality.Immortal, new Color(0.5f, 0.5f, 0f)}
        };

        public abstract string GetName();


        public Color GetCurrentColor()
        {
            return _colorWithQuality[Quality];
        }

        public float GetPrice()
        {   
            return Random.Range(_minPrice, _maxPrice);;
        }
    }
}