using Sources.Modules.Weapon.WeaponData;
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

        public override void Init(BaseWeaponData weaponData)
        {
            base.Init(weaponData);
            _sellButtonText.text = $"Продать \n+{weaponData.GetCurrentPrice()}$";
        }

        private void OnSellButtonClick()
        {
            
        }
    }
}