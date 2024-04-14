using Sources.Modules.Weapon.Scripts;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;

namespace Sources.Modules.Common.Scripts
{
    [RequireComponent(typeof(WeaponView))]
    public abstract class Weapon : MonoBehaviour
    {
        public BaseWeaponData Data { get; private set; }
        
        protected WeaponView View { get; private set; }
        public float Price { get; private set; } = -1;
        
        private void Awake()
        {
            View = GetComponent<WeaponView>();
        }
        
        public void Init(BaseWeaponData weaponData)
        {
            Data = weaponData;
            View.UpdateData(Data);

            Price = weaponData.GetPrice();
        }
        
        public virtual void Init(Weapon weapon)
        {
            Data = weapon.Data;
            View.UpdateData(Data);

            Price = weapon.Price;
        }
    }
}