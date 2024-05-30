using System;
using Lean.Localization;
using Sources.Modules.MiniGames.FlappyChicken.Interfaces;
using Sources.Modules.MiniGames.Scripts;
using Sources.Modules.Utils;
using TMPro;
using UnityEngine;
using Zenject;

namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    public class FlappyChickenView : MiniGamesView, IFlappyChickenView
    {
        [SerializeField] private CanvasGroup _endCanvasGroup;
        [SerializeField] private CanvasGroup _obstaclesCanvasGroup;
        [SerializeField] private TMP_Text _earnedText;
        
        private int _currentCoins;
        private FlappyChickenRoot _chickenRoot;
        private bool _isEnable;

        public event Action EnterButtonClick;
        public event Action Disable;
        

        [Inject]
        public void Construct(FlappyChickenRoot chickenRoot)
        {
            _chickenRoot = chickenRoot;
            CanvasGroupUtil.Disable(_obstaclesCanvasGroup);
        }

        private void Start()
        {
            Disable?.Invoke();
            _earnedText.text = $"{LeanLocalization.GetTranslationText("Earned")}: {_currentCoins}";
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _chickenRoot.CoinAdded += OnCoinAdded;
            _chickenRoot.Disabled += OnPlayerEnetered;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _chickenRoot.Disabled -= OnPlayerEnetered;
            _chickenRoot.CoinAdded -= OnCoinAdded;
        }

        protected override void OnEnterButtonClick()
        {
            base.OnEnterButtonClick();
            EnterButtonClick?.Invoke();
            CanvasGroupUtil.Enable(_obstaclesCanvasGroup);
            _isEnable = true;
        }

        protected override void OnExitButtonClick()
        {
            base.OnExitButtonClick();
            CanvasGroupUtil.Disable(_obstaclesCanvasGroup);
            _currentCoins = 0;
            _earnedText.text = $"{LeanLocalization.GetTranslationText("Earned")}: {_currentCoins}";
            CanvasGroupUtil.Disable(_endCanvasGroup);
            _isEnable = false;
        }

        private void OnPlayerEnetered()
        {
            if (_isEnable)
                CanvasGroupUtil.Enable(_endCanvasGroup);
            
        }

        private void OnCoinAdded(int coins)
        {
            if (_isEnable)
            {
                _currentCoins += coins;
                _earnedText.text = $"{LeanLocalization.GetTranslationText("Earned")}: {_currentCoins}";
            }
        }
    }
}
