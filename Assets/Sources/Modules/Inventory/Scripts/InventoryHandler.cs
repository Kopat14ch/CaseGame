using System;
using System.Collections.Generic;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Inventory.Interfaces;
using Sources.Modules.Weapon.Scripts;
using Zenject;

namespace Sources.Modules.Inventory.Scripts
{
    public class InventoryHandler : IDisposable, IInventoryHandler, IInitializable
    {
        private readonly InventoryFactory _inventoryFactory;
        private readonly ICaseOpenerHandler _caseOpenerHandler;
        private readonly ICaseOpenerView _caseOpenerView;
        private readonly IInventoryView _view;
        private readonly List<WeaponRoot> _weaponRoots;

        private WeaponRoot _newWeapon;

        public event Action<WeaponRoot> WeaponSold; 
        public event Action<WeaponRoot> WeaponAdded;

        public InventoryHandler(InventoryFactory inventoryFactory, ICaseOpenerHandler caseOpenerHandler, ICaseOpenerView caseOpenerView, IInventoryView view)
        {
            _weaponRoots = new List<WeaponRoot>();
            
            _view = view;
            _inventoryFactory = inventoryFactory;
            _caseOpenerHandler = caseOpenerHandler;
            _caseOpenerView = caseOpenerView;

            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
            _caseOpenerView.SellButtonClicked += OnCaseOpenerSellButtonClicked;
            _view.SellButtonClicked += WeaponSell;
        }
        
        public void Initialize()
        {
            foreach (var weaponRoot in _inventoryFactory.Initialize())
                WeaponAdd(weaponRoot);
        }

        public void Dispose()
        {
            _caseOpenerHandler.ScrollComplete -= OnScrollComplete;
            _caseOpenerView.SellButtonClicked -= OnCaseOpenerSellButtonClicked;
            _view.SellButtonClicked -= WeaponSell;
        }
        
        private void OnScrollComplete(Common.Scripts.Weapon weapon)
        {
            _newWeapon = _inventoryFactory.Create(weapon);
            WeaponAdd(_newWeapon);
        }

        private void OnCaseOpenerSellButtonClicked()
        {
            WeaponSell(_newWeapon);
        }
        
        private void WeaponAdd(WeaponRoot weaponRoot)
        {
            WeaponAdded?.Invoke(weaponRoot);
            _weaponRoots.Add(weaponRoot);
            weaponRoot.Clicked += OnWeaponRootClicked;
        }

        private void WeaponSell(WeaponRoot weaponRoot)
        {
            WeaponSold?.Invoke(weaponRoot);
            
            _weaponRoots.Remove(weaponRoot);
            weaponRoot.Clicked -= OnWeaponRootClicked;
            weaponRoot.Sell();
        }

        private void OnWeaponRootClicked(WeaponRoot weaponRoot)
        {
            _view.UpdateWeapon(weaponRoot);
        }
    }
}