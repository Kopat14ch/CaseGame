﻿using System.Collections.Generic;
using Sources.Modules.Weapon.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.Weapon.WeaponData
{
    public abstract class BaseWeaponData : ScriptableObject
    {
        [field: SerializeField] public string SkinName { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public WeaponQuality Quality { get; private set; }
        
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
        
        private float _currentPrice;
        private Color _myColor;


        public Color GetCurrentColor()
        {
            return _colorWithQuality[Quality];
        }

        public abstract string GetName();

        public float GetCurrentPrice()
        {
            if (_currentPrice <= 0)
                _currentPrice = Random.Range(_minPrice, _maxPrice);
            
            return _currentPrice;
        }
    }
}