using Sources.Modules.YandexSDK.Scripts.Leaderboard.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.YandexSDK.Scripts.Leaderboard
{
    public class LeaderboardView : MonoBehaviour, ILeaderboardView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openButton;
        [SerializeField] private CanvasGroup _myCanvasGroup;

        public Button CloseButton => _closeButton;
        public Button OpenButton => _openButton;
        public CanvasGroup MyCanvasGroup => _myCanvasGroup;
    }
}