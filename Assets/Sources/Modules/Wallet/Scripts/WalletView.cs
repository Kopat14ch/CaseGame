using System;
using System.Collections;
using DG.Tweening;
using Sources.Modules.Wallet.Interfaces;
using TMPro;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _smoothTime;
        
        private IWalletHandler _walletHandler;
        private Tween _moneyTween;
        private float _currentValue;

        public void Init(IWalletHandler walletHandler)
        {
            _walletHandler = walletHandler;
        }
        
        private void OnEnable()
        {
            _walletHandler.MoneyChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            _walletHandler.MoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(float newValue)
        {
            if (_moneyTween is {active: true})
                _moneyTween.Kill();

            float startValue = _currentValue;
            _moneyTween = DOTween.To(() => startValue, UpdateMoneyText, newValue, _smoothTime);
        }
        
        private void UpdateMoneyText(float value)
        {
            _text.text = $"{Math.Round(value, 2)}$";
            _currentValue = value;
        }
    }
}