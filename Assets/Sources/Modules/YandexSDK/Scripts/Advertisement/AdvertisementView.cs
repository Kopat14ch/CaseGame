using System;
using Lean.Localization;
using Sources.Modules.Utils;
using Sources.Modules.YandexSDK.Scripts.Advertisement.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Sources.Modules.YandexSDK.Scripts.Advertisement
{
    public class AdvertisementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private CanvasGroup _canvasGroup;
        private IAsyncTimerAd _asyncTimerAd;

        [Inject]
        public void Construct(IAsyncTimerAd asyncTimerAd)
        {
            _asyncTimerAd = asyncTimerAd;
            Hide();
        }

        private void OnEnable()
        {
            _asyncTimerAd.AsyncTimerAdUpdated += UpdateText;
            _asyncTimerAd.AsyncTimerAdStart += Show;
            _asyncTimerAd.AsyncTimerAdStop += Hide;
        }

        private void OnDisable()
        {
            _asyncTimerAd.AsyncTimerAdUpdated -= UpdateText;
            _asyncTimerAd.AsyncTimerAdStart -= Show;
            _asyncTimerAd.AsyncTimerAdStop -= Hide;
        }

        private void UpdateText(int value)
        {
            _text.text = $"{LeanLocalization.GetTranslationText("Ad in")}: {value}";
        }

        private void Hide()
        {
            CanvasGroupUtil.Disable(_canvasGroup);
        }

        private void Show()
        {
            CanvasGroupUtil.Enable(_canvasGroup);
        }
    }
}