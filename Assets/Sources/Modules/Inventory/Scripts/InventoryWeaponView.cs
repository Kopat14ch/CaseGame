using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Inventory.Scripts
{
    public class InventoryWeaponView : Common.Scripts.Weapon
    {
        [SerializeField] private Button _sellButton;
        [SerializeField] private TMP_Text _sellButtonText;
        
        private void OnEnable()
        {
            _sellButton.onClick.AddListener(OnSellButtonClick);
        }

        private void OnDisable()
        {
            _sellButton.onClick.RemoveListener(OnSellButtonClick);
        }

        public override void Init(Common.Scripts.Weapon weapon)
        {
            base.Init(weapon);
            _sellButtonText.text = $"Продать \n+{Math.Round(weapon.Price, 2)}$";  
        }

        private void OnSellButtonClick()
        {
            
        }
    }
}