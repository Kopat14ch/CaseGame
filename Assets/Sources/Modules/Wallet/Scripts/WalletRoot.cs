using Sources.Modules.Wallet.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Wallet.Scripts
{
    [RequireComponent(typeof(WalletView))]
    public class WalletRoot : MonoBehaviour, IWalletRoot
    {
        private WalletView _walletView;
        private WalletHandler _walletHandler;

        [Inject]
        public void Construct(WalletHandler walletHandler)
        {
            _walletHandler = walletHandler;
            
            _walletView = GetComponent<WalletView>();
            _walletView.Init(_walletHandler);
        }

        private void Start()
        {
            _walletHandler.Init();
        }

        public bool TryTakeMoney(float money)
        {
           return _walletHandler.TryTakeMoney(money);
        }

        public void AddMoney(float money)
        {
            _walletHandler.AddMoney(money);
        }
    }
}