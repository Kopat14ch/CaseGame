using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.YandexSDK.Scripts.Leaderboard.Interfaces
{
    public interface ILeaderboardView
    {
        public Button CloseButton { get; }
        public Button OpenButton { get; }
        public CanvasGroup MyCanvasGroup { get; }
    }
}