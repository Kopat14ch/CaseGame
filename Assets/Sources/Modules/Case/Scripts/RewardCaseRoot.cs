using Sources.Modules.YandexSDK.Scripts.Advertisement;
using Sources.Modules.YandexSDK.Scripts.Advertisement.Interfaces;
using Zenject;

namespace Sources.Modules.Case.Scripts
{
    public class RewardCaseRoot : CaseRoot
    {
        private IAdvertisementHandler _advertisementHandler;

        [Inject]
        public void Construct(IAdvertisementHandler advertisementHandler)
        {
            _advertisementHandler = advertisementHandler;
        }

        protected override void OnOpenButtonClicked()
        {
#if UNITY_EDITOR
            CaseOpenerRoot.Open(Data);
            return;
#endif
            
            _advertisementHandler.ShowVideoAd(onRewardedCallback: () => CaseOpenerRoot.Open(Data));
            
        }
    }
}