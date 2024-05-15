using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.MiniGames.Clicker.Interfaces;

namespace Sources.Modules.YandexSDK.Scripts.Advertisement
{
    public class AdvertisementHandler
    {
        private readonly ICaseOpenerView _caseOpenerView;
        private readonly ICoinRoot _coinRoot;

        private const int ClicksForAd = 20;

        public AdvertisementHandler(ICaseOpenerView caseOpenerView, ICoinRoot coinRoot)
        {
            _caseOpenerView = caseOpenerView;
            _coinRoot = coinRoot;
        }
    }
}