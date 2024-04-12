

using UnityEngine;

namespace Sources.Modules.Inventory.Scripts
{
    public class InventoryFactory
    {
        private readonly InventoryContent _inventoryContent;

        public InventoryFactory(InventoryContent inventoryContent)
        {
            _inventoryContent = inventoryContent;
        }

        public void Create(Common.Scripts.Weapon weapon)
        {
            Object.Instantiate(weapon, _inventoryContent.transform);
        }
    }
}