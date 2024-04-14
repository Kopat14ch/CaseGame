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
        
        private void Awake()
        {
            View = GetComponent<WeaponView>();
        }
        
        public virtual void Init(BaseWeaponData weaponData)
        {
            Data = weaponData;
            View.UpdateData(Data);
        }
        

    }
}