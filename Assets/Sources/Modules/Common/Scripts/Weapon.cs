using System;
using Sources.Modules.Weapon.Scripts;
using Sources.Modules.Weapon.Scripts.WeaponData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.Common.Scripts
{
    [RequireComponent(typeof(WeaponView))]
    public abstract class Weapon : MonoBehaviour
    {
        public BaseWeaponData Data { get; private set; }
        
        protected WeaponView View { get; private set; }
        public float Price { get; private set; } = -1;
        public int Id { get; private set; }
        
        private void Awake()
        {
            View = GetComponent<WeaponView>();
        }
        
        public void Init(BaseWeaponData weaponData, int id, float price = 0)
        {
            Data = weaponData;
            View.UpdateData(Data);

            Id = id;
            Price = price <= 0 ? weaponData.GetPrice() : price;
        }
        
        public virtual void Init(Weapon weapon)
        {
            Data = weapon.Data;
            View.UpdateData(Data);

            Price = weapon.Price;

            Price = weapon.Price;
            Id = weapon.Id;
        }

        public void UpdateId()
        {
            Id = Random.Range(int.MinValue, int.MaxValue);
        }
    }
}