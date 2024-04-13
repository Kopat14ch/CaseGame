using System;

namespace Sources.Modules.Wallet.Interfaces
{
    public interface IWalletHandler
    {
        public event Action<float> MoneyChanged; 
    }
}