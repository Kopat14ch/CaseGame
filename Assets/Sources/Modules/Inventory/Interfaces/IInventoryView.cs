using System;
using Sources.Modules.Weapon.Scripts;

namespace Sources.Modules.Inventory.Interfaces
{
    public interface IInventoryView
    {
        public event Action<WeaponRoot> SellButtonClicked;

        public void UpdateWeapon(WeaponRoot weaponRoot);
    }
}