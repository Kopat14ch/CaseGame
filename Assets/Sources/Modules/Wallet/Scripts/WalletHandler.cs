using System;
using Sources.Modules.Wallet.Interfaces;

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
        
        public void Init()
        {
            Money = StartMoney;
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