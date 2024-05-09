using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Sources.Modules.Weapon.Scripts;
using Sources.Modules.Weapon.Scripts.WeaponData;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
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

        public async Task<WeaponRoot[]> Initialize()
        {
            await UniTask.WaitUntil(() => YandexSaves.Instance.IsLoaded);
            
            return InitWeapons(YandexSaves.Instance.Load().WeaponsData);
        }

        private WeaponRoot[] InitWeapons(WeaponSaveData[] weaponSaveDatas)
        {
            List<WeaponRoot> weaponRoots = new List<WeaponRoot>();
            
            foreach (var data in weaponSaveDatas)
            {
                BaseWeaponData weaponData = Resources.Load<BaseWeaponData>(data.PathToFile);
                
                WeaponRoot weaponRoot = _container.InstantiatePrefabForComponent<WeaponRoot>(_prefab, _inventoryContent.transform);
           
                weaponRoot.Init(weaponData, data.Id, data.Price);

                weaponRoots.Add(weaponRoot);
            }
            
            return weaponRoots.ToArray();
        }
    }
}