using System;
using DG.Tweening;
using Sources.Modules.Wallet.Interfaces;
using UnityEngine;

namespace Sources.Modules.MiniGames.Clicker.Scripts
{
    public class ClickerHandler : IDisposable
    {
        private readonly CoinRoot _coinRoot;
        private readonly IWalletRoot _walletRoot;
        private readonly Transform _coinTransform;
        private readonly Vector3 _coinBaseScale;
        private readonly Vector3 _clickScale;
        private readonly float _durationScale;
        private readonly int _moneyPerClick;

        public ClickerHandler(CoinRoot coinRoot, IWalletRoot walletRoot)
        {
            _clickScale = new Vector3(0.9f, 0.9f, 0.9f);
            _durationScale = .1f;
            _moneyPerClick = 2;
            
            _coinRoot = coinRoot;
            _walletRoot = walletRoot;
            _coinTransform = coinRoot.transform;
            _coinBaseScale = _coinTransform.localScale;
            
            _coinRoot.Clicked += OnCoinRootClicked;
        }
        
        private void OnCoinRootClicked()
        {
            _walletRoot.AddMoney(_moneyPerClick);
            
            _coinTransform.DOScale(_clickScale, _durationScale)
                .OnComplete(() => _coinTransform.DOScale(_coinBaseScale, _durationScale).SetEase(Ease.InOutSine));
        }

        public void Dispose()
        {
            _coinRoot.Clicked -= OnCoinRootClicked;
        }
    }
}