using Sources.Modules.Weapon.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.Weapon.WeaponData
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData", order = 0)]
    public class WeaponData : ScriptableObject
    {
        [field: SerializeField] public WeaponName Name { get; private set; }
        [field: SerializeField] public string SkinName { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Quality Quality { get; private set; }
        
        [SerializeField] private float _minPrice;
        [SerializeField] private float _maxPrice;

        private float _currentPrice;

        public float GetCurrentPrice()
        {
            if (_currentPrice <= 0)
                _currentPrice = Random.Range(_minPrice, _maxPrice);
            
            return _currentPrice;
        }
    }
}