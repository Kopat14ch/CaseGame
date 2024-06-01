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
        private const int ClicksForAd = 50;
        private const int CaseOpenForAd = 2;
        private const int SecondsForAd = 60;
        
        private readonly ICaseOpenerView _caseOpenerView;
        private readonly ICoinRoot _coinRoot;
        private readonly ISoundSettingsHandler _soundSettingsHandler;
        
        private int _currentClicks;
        private int _currentOpen;
        private float _currentAdTime;
        private bool _canShowAd;

        public event Action<int> AsyncTimerAdUpdated;
        public event Action AsyncTimerAdStart;
        public event Action AsyncTimerAdStop;

        public AdvertisementHandler(ICaseOpenerView caseOpenerView, ICoinRoot coinRoot, ISoundSettingsHandler soundSettingsHandler)
        {
            _caseOpenerView = caseOpenerView;
            _coinRoot = coinRoot;
            _soundSettingsHandler = soundSettingsHandler;
            _canShowAd = true;

            _caseOpenerView.SellButtonClicked += OnCaseOpenerAction;
            _caseOpenerView.TakeButtonClicked += OnCaseOpenerAction;

            _coinRoot.Clicked += OnClickerClicked;
        }

        private void OnCaseOpenerAction()
        {
            if (_currentOpen >= CaseOpenForAd)
            {
                ShowAd(onOpenCallback: () => _currentOpen = 0);
                return;
            }

            _currentOpen++;
        }

        private async void OnClickerClicked()
        {
            if (_currentClicks >= ClicksForAd) 
            {
                if (_canShowAd == false)
                    return;
                
                ShowAd(onOpenCallback: () => _currentClicks = 0);
                return;
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
                AsyncTimerAdStop?.Invoke();
            }, onCloseCallback: () =>
            {
                Time.timeScale = 1f;
                AudioListener.volume = _soundSettingsHandler.LastVolume;
            }, onRewardedCallback: onRewardedCallback, onErrorCallback: onErrorCallback);
        }

        public async void ShowAd(Action onOpenCallback = null)
        {
            await AsyncTimerAd();
            
            AsyncWaitingAd().Forget();
            _canShowAd = false;
            
#if UNITY_EDITOR
            return;
#endif 
            
            InterstitialAd.Show(() =>
            {
                onOpenCallback?.Invoke();
                Time.timeScale = 0f;
                AudioListener.volume = 0;
                AsyncTimerAdStop?.Invoke();
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
        }

        private async UniTaskVoid AsyncWaitingAd()
        {
            if (_canShowAd == false)
            {
                _currentAdTime = 0;
                return;
            }
            
            while (true)
            {
                if (CanShowAd())
                    break;

                _currentAdTime += Time.deltaTime;
                
                await UniTask.Yield();
            }
            
            _currentAdTime = 0;
            _canShowAd = true;
        }

        private bool CanShowAd() => _currentAdTime >= SecondsForAd;
    }
}