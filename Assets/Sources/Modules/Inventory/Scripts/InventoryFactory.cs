using Sources.Modules.Weapon.Scripts;
using Zenject;

namespace Sources.Modules.Inventory.Scripts
{
    public class InventoryFactory
    {
        private readonly InventoryContent _inventoryContent;
        private readonly WeaponRoot _prefab;
        private readonly DiContainer _container;

        public InventoryFactory(InventoryContent inventoryContent, WeaponRoot prefab, DiContainer container)
        {
            _inventoryContent = inventoryContent;
            _prefab = prefab;
            _container = container;
        }

        public WeaponRoot Create(Common.Scripts.Weapon weapon)
        {
           WeaponRoot weaponRoot = _container.InstantiatePrefabForComponent<WeaponRoot>(_prefab, _inventoryContent.transform);
           
           weaponRoot.Init(weapon);

           return weaponRoot;
        }
    }
}