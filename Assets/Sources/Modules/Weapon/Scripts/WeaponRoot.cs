using System;
using Sources.Modules.Wallet.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.Weapon.Scripts
{
    [RequireComponent(typeof(WeaponView))]
    public class WeaponRoot : Common.Scripts.Weapon
    {
        private IWalletRoot _walletRoot;
        private Button _button;

        public event Action<WeaponRoot> Clicked; 

        [Inject]
        public void Construct(IWalletRoot walletRoot)
        {
            _walletRoot = walletRoot;
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if (_button != null)
                _button.onClick.AddListener(OnClicked);
            
        }

        private void OnDisable()
        {
            if (_button != null)
                _button.onClick.RemoveListener(OnClicked);
        }

        public void Sell()
        {
            _walletRoot.AddMoney(Data.GetCurrentPrice());
            Destroy(gameObject);
        }

        private void OnClicked()
        {
            Clicked?.Invoke(this);
        }
    }
}
