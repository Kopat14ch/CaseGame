using System;
using Agava.YandexGames;
using Sources.Modules.Utils;
using Sources.Modules.YandexSDK.Scripts.Leaderboard.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.YandexSDK.Scripts.Leaderboard
{
    public class LeaderboardViewHandler : IDisposable , ILeaderboardViewHandler
    {
        private readonly Button _closeButton;
        private readonly Button _openButton;
        private readonly CanvasGroup _canvasGroup;

        public event Action Opened;

        public LeaderboardViewHandler(ILeaderboardView leaderboardView)
        {
            _closeButton = leaderboardView.CloseButton;
            _openButton = leaderboardView.OpenButton;
            _canvasGroup = leaderboardView.MyCanvasGroup;

            _closeButton.onClick.AddListener(OnCloseButtonClick);
            _openButton.onClick.AddListener(OnOpenButtonClick);
        }

        public void Dispose()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
            _openButton.onClick.RemoveListener(OnOpenButtonClick);
        }
        
        private void OnCloseButtonClick()
        {
            CanvasGroupUtil.Disable(_canvasGroup);
        }
        
        private void OnOpenButtonClick()
        {
            Opened?.Invoke();

#if UNITY_EDITOR == false
            if (PlayerAccount.IsAuthorized)
                CanvasGroupUtil.Enable(_canvasGroup);
            return;
#endif
            
            CanvasGroupUtil.Enable(_canvasGroup);
        }
    }
}