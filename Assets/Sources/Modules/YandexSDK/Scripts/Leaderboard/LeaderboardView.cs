using Sources.Modules.YandexSDK.Scripts.Leaderboard.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.YandexSDK.Scripts.Leaderboard
{
    public class LeaderboardView : MonoBehaviour, ILeaderboardView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _authorizedButton;
        [SerializeField] private CanvasGroup _myCanvasGroup;
        [SerializeField] private CanvasGroup _baseCanvasGroup;
        [SerializeField] private CanvasGroup _authorizedCanvasGroup;

        public Button CloseButton => _closeButton;
        public Button OpenButton => _openButton;
        public Button AuthorizedButton => _authorizedButton;
        public CanvasGroup MyCanvasGroup => _myCanvasGroup;
        public CanvasGroup BaseCanvasGroup => _baseCanvasGroup;
        public CanvasGroup AuthorizedCanvasGroup => _authorizedCanvasGroup;
    }
}