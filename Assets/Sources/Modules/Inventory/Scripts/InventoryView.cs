using System;
using Sources.Modules.Inventory.Interfaces;
using Sources.Modules.Weapon.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Inventory.Scripts
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private InventoryWeaponView _weaponView;
        [SerializeField] private Button _sellButton;
        [SerializeField] private CanvasGroup _canvasGroup;

        private WeaponRoot _currentWeapon;

        private bool _isActive;

        public event Action<WeaponRoot> SellButtonClicked; 

        public void UpdateWeapon(WeaponRoot weaponRoot)
        {
            if (_isActive == false)
            {
                _canvasGroup.alpha = 1;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
                _isActive = true;
            }
            
            _currentWeapon = weaponRoot;
            _weaponView.Init(_currentWeapon);
        }

        private void OnEnable()
        {
            _sellButton.onClick.AddListener(OnSellButtonClick);
        }

        private void OnDisable()
        {
            _sellButton.onClick.RemoveListener(OnSellButtonClick);
        }

        private void OnSellButtonClick()
        {
            SellButtonClicked?.Invoke(_currentWeapon);
            
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            
            _isActive = false;
        }
    }
}