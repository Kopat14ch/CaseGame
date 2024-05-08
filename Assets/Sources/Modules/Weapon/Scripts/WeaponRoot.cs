using System;
using Sources.Modules.Wallet.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.Weapon.Scripts
{
    [RequireComponent(typeof(WeaponView))]
    [RequireComponent(typeof(Button))]
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
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void Sell()
        {
            _walletRoot.AddMoney(Price);
            Destroy(gameObject);
        }

        private void OnClicked()
        {
            Clicked?.Invoke(this);
        }
    }
}
