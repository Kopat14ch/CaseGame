using System;

using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.MiniGames.Clicker.Interfaces;
using Sources.Modules.Settings.Interfaces;
using Sources.Modules.YandexSDK.Scripts.Advertisement.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.YandexSDK.Scripts.Advertisement
{
    public class AdvertisementHandler : IDisposable, IAdvertisementHandler, IAsyncTimerAd
    {
        private const int ClicksForAd = 100;
        private const int ClicksForAdVideo = 320;
        private const int CaseOpenForAd = 2;
        
        private readonly ICaseOpenerView _caseOpenerView;
        private readonly ICaseOpenerHandler _caseOpenerHandler;
        private readonly ICoinRoot _coinRoot;
        private readonly ISoundSettingsHandler _soundSettingsHandler;

        private bool _canShowClickerAd;
        private int _currentClicks;
        private int _currentOpen;

        public event Action<int> AsyncTimerAdUpdated;
        public event Action AsyncTimerAdStart;
        public event Action AsyncTimerAdStop;

        public AdvertisementHandler(ICaseOpenerView caseOpenerView, ICaseOpenerHandler caseOpenerHandler, ICoinRoot coinRoot, ISoundSettingsHandler soundSettingsHandler)
        {
            _caseOpenerView = caseOpenerView;
            _caseOpenerHandler = caseOpenerHandler;
            _coinRoot = coinRoot;
            _soundSettingsHandler = soundSettingsHandler;
            _canShowClickerAd = true;

            _caseOpenerView.SellButtonClicked += OnCaseOpenerAction;
            _caseOpenerView.OpenAgainButtonClicked += OnCaseOpenerAction;
            _caseOpenerView.TakeButtonClicked += OnCaseOpenerAction;

            _coinRoot.Clicked += OnClickerClicked;
        }

        private void OnCaseOpenerAction()
        {
            if (_currentOpen >= CaseOpenForAd)
                ShowVideoAd(onOpenCallback: () => _currentOpen = 0, onErrorCallback: _ => ShowAd());

            _currentOpen++;
        }

        private async void OnClickerClicked()
        {
            if (_currentClicks >= ClicksForAdVideo)
            {
                await AsyncTimerAd();
                
                ShowVideoAd(() =>
                {
                    _currentClicks = 0;
                    _canShowClickerAd = true;
                },onErrorCallback: _ =>
                {
                    _currentClicks = 0;
                    _canShowClickerAd = true;
                    ShowAd();
                });
            }
            else if (_currentClicks >= ClicksForAd && _canShowClickerAd)
            {
                await AsyncTimerAd();
                
                ShowAd(onOpenCallback: () => _canShowClickerAd = false);
            }

            _currentClicks++;
        }

        public void ShowVideoAd(Action onOpenCallback = null, Action<string> onErrorCallback = null, Action onRewardedCallback = null)
        {
#if UNITY_EDITOR
            return;
#endif 
            
            VideoAd.Show(() =>
            {
                onOpenCallback?.Invoke();
                Time.timeScale = 0f;
                AudioListener.volume = 0;
            }, onCloseCallback: () =>
            {
                Time.timeScale = 1f;
                AudioListener.volume = _soundSettingsHandler.LastVolume;
            }, onRewardedCallback: onRewardedCallback, onErrorCallback: onErrorCallback);
        }

        public void ShowAd(Action onOpenCallback = null)
        {
#if UNITY_EDITOR
            return;
#endif 
            
            InterstitialAd.Show(() =>
            {
                onOpenCallback?.Invoke();
                Time.timeScale = 0f;
                AudioListener.volume = 0;
            }, onCloseCallback: _ =>
            {
                Time.timeScale = 1f;
                AudioListener.volume = _soundSettingsHandler.LastVolume;
            });
        }
        
        public void Dispose()
        {
            _caseOpenerView.SellButtonClicked -= OnCaseOpenerAction;
            _caseOpenerView.OpenAgainButtonClicked -= OnCaseOpenerAction;
            _caseOpenerView.TakeButtonClicked -= OnCaseOpenerAction;

            _coinRoot.Clicked -= OnClickerClicked;
        }

        private async UniTask AsyncTimerAd()
        {
            int seconds = 3;

            AsyncTimerAdStart?.Invoke();
            Time.timeScale = 0f;
            
            while (seconds >= 0)
            {
                AsyncTimerAdUpdated?.Invoke(seconds);
                seconds--;
                await UniTask.WaitForSeconds(1, true);
            }

            AsyncTimerAdStop?.Invoke();
        }
    }
}