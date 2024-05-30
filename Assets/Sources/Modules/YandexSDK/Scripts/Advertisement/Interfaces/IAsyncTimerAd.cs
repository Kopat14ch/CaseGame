using System;

namespace Sources.Modules.YandexSDK.Scripts.Advertisement.Interfaces
{
    public interface IAsyncTimerAd
    {
        public event Action<int> AsyncTimerAdUpdated;
        public event Action AsyncTimerAdStart;
        public event Action AsyncTimerAdStop;
    }
}