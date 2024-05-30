using System;

namespace Sources.Modules.YandexSDK.Scripts.Advertisement.Interfaces
{
    public interface IAdvertisementHandler
    {
        public void ShowVideoAd(Action onOpenCallback = null, Action<string> onErrorCallback = null, Action onRewardedCallback = null);
        public void ShowAd(Action onOpenCallback = null);
    }
}