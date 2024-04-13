namespace Sources.Modules.Wallet.Interfaces
{
    public interface IWalletRoot
    {
        public bool TryTakeMoney(float money);
        public void AddMoney(float money);
    }
}