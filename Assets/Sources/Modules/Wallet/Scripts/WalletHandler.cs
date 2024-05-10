using System;
using Cysharp.Threading.Tasks;
using Sources.Modules.Wallet.Interfaces;
using Sources.Modules.YandexSDK.Scripts;

namespace Sources.Modules.Wallet.Scripts
{
    public class WalletHandler : IWalletHandler
    {
        private float _money;

        private const float StartMoney = 10000;

        private float Money
        {
            get => _money;
            set
            {
                if (value <= 0) 
                    return;
        
                _money = value;
                MoneyChanged?.Invoke(_money);
            }
        }

        public event Action<float> MoneyChanged;
        
        public async void Init()
        {
            await UniTask.WaitUntil(() => YandexSaves.Instance.IsLoaded);
            
            Money = YandexSaves.Instance.Load().Money;
        }

        public bool TryTakeMoney(float value)
        {
            if (Money - value < 0)
                return false;

            Money -= value;
            return true;
        }

        public void AddMoney(float value)
        {
            Money += value;
        }


    }
}