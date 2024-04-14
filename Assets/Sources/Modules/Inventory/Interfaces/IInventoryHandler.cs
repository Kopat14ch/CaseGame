using System;
using Sources.Modules.Weapon.Enums;

namespace Sources.Modules.Inventory.Interfaces
{
    public interface IInventoryHandler
    {
        public event Action<WeaponQuality> WeaponSelled;
    }
}