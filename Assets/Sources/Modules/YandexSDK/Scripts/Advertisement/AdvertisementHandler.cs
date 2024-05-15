using System;
using Agava.YandexGames;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.MiniGames.Clicker.Interfaces;
using Sources.Modules.Settings.Interfaces;
using Sources.Modules.Settings.Scripts.Sound;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts.Advertisement
{
    public class AdvertisementHandler : IDisposable
    {
        private const int ClicksForAd = 20;
        private const int ClicksForAdVideo = 50;
        private const int CaseOpenForAd = 2;
        
        private readonly ICaseOpenerView _caseOpenerView;
        private readonly ICaseOpenerHandler _caseOpenerHandler;
        private readonly ICoinRoot _coinRoot;
        private readonly ISoundSettingsHandler _soundSettingsHandler;

        private bool _canShowClickerAd;
        private int _currentClicks;
        private int _currentOpen;

        public AdvertisementHandler(ICaseOpenerView caseOpenerView, ICaseOpenerHandler caseOpenerHandler, ICoinRoot coinRoot, ISoundSettingsHandler soundSettingsHandler)
        {
            _caseOpenerView = caseOpenerView;
            _caseOpenerHandler = caseOpenerHandler;
            _coinRoot = coinRoot;
            _soundSettingsHandler = soundSettingsHandler;
            _canShowClickerAd = true;

            _caseOpenerView.SellButtonClicked += OnCaseOpenerAction;
            _caseOpenerView.OpenAgainButtonClicked += OnCaseOpenerAction;

            _caseOpenerHandler.Opened += OnCaseOpenerAction;

            _coinRoot.Clicked += OnClickerClicked;
        }

        private void OnCaseOpenerAction()
        {
            if (_currentOpen >= CaseOpenForAd)
                ShowVideoAd(onOpenCallback: () => _currentOpen = 0, onErrorCallback: _ => ShowAd());

            _currentOpen++;
        }

        private void OnClickerClicked()
        {
            Debug.Log(_soundSettingsHandler.LastVolume);
            
            if (_currentClicks >= ClicksForAdVideo)
            {
                ShowVideoAd(() =>
                {
                    _currentClicks = 0;
                    _canShowClickerAd = true;
                });
            }
            else if (_currentClicks >= ClicksForAd && _canShowClickerAd)
            {
                ShowAd(onOpenCallback: () => _canShowClickerAd = false);
            }

            _currentClicks++;
        }

        private void ShowVideoAd(Action onOpenCallback = null, Action<string> onErrorCallback = null)
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
            }, onErrorCallback: onErrorCallback);
        }

        private void ShowAd(Action onOpenCallback = null)
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

            _caseOpenerHandler.Opened -= OnCaseOpenerAction;

            _coinRoot.Clicked -= OnClickerClicked;
        }
    }
}