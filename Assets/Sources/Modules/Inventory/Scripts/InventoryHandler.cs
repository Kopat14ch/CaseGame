﻿using System;
using System.Collections.Generic;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Inventory.Interfaces;
using Sources.Modules.Weapon.Scripts;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Inventory.Scripts
{
    public class InventoryHandler : IDisposable, IInventoryHandler, IInitializable
    {
        private readonly InventoryFactory _inventoryFactory;
        private readonly ICaseOpenerHandler _caseOpenerHandler;
        private readonly ICaseOpenerView _caseOpenerView;
        private readonly IInventoryView _view;

        private WeaponRoot _newWeapon;

        public event Action<WeaponRoot> WeaponSold; 
        public event Action<WeaponRoot> WeaponAdded;

        public InventoryHandler(InventoryFactory inventoryFactory, ICaseOpenerHandler caseOpenerHandler, ICaseOpenerView caseOpenerView, IInventoryView view)
        {
            _view = view;
            _inventoryFactory = inventoryFactory;
            _caseOpenerHandler = caseOpenerHandler;
            _caseOpenerView = caseOpenerView;

            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
            _caseOpenerView.SellButtonClicked += OnCaseOpenerSellButtonClicked;
            _view.SellButtonClicked += WeaponSell;
        }
        
        public async void Initialize()
        {
            WeaponRoot[] weaponRoots = await _inventoryFactory.Initialize();
            
            foreach (var weaponRoot in weaponRoots)
                WeaponAdd(weaponRoot, false);
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
        
        private void WeaponAdd(WeaponRoot weaponRoot, bool wantInvoke = true)
        {
            if (wantInvoke) 
                WeaponAdded?.Invoke(weaponRoot);

            weaponRoot.Clicked += OnWeaponRootClicked;
        }

        private void WeaponSell(WeaponRoot weaponRoot)
        {
            WeaponSold?.Invoke(weaponRoot);
            
            weaponRoot.Clicked -= OnWeaponRootClicked;
            weaponRoot.Sell();
        }

        private void OnWeaponRootClicked(WeaponRoot weaponRoot)
        {
            _view.UpdateWeapon(weaponRoot);
        }
    }
}