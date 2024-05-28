using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.YandexSDK.Scripts.Leaderboard.Interfaces
{
    public interface ILeaderboardView
    {
        public Button CloseButton { get; }
        public Button OpenButton { get; }
        public Button AuthorizedButton { get; }
        public CanvasGroup MyCanvasGroup { get; }
        public CanvasGroup BaseCanvasGroup { get; }
        public CanvasGroup AuthorizedCanvasGroup { get; }
    }
}