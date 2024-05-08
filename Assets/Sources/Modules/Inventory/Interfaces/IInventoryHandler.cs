using System;
using Sources.Modules.Weapon.Enums;
using Sources.Modules.Weapon.Scripts;

namespace Sources.Modules.Inventory.Interfaces
{
    public interface IInventoryHandler
    {
        public event Action<WeaponRoot> WeaponSold;
        public event Action<WeaponRoot> WeaponAdded;
    }
}