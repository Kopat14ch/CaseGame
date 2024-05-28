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
        private readonly Button _authorizedButton;
        private readonly CanvasGroup _myCanvasGroup;
        private readonly CanvasGroup _baseCanvasGroup;
        private readonly CanvasGroup _authorizedCanvasGroup;

        public event Action Opened;
        public event Action Authorized;

        public LeaderboardViewHandler(ILeaderboardView leaderboardView)
        {
            _closeButton = leaderboardView.CloseButton;
            _openButton = leaderboardView.OpenButton;
            _authorizedButton = leaderboardView.AuthorizedButton;
            _myCanvasGroup = leaderboardView.MyCanvasGroup;
            _baseCanvasGroup = leaderboardView.BaseCanvasGroup;
            _authorizedCanvasGroup = leaderboardView.AuthorizedCanvasGroup;

            _closeButton.onClick.AddListener(DisableAll);
            _openButton.onClick.AddListener(OnOpenButtonClick);
            _authorizedButton.onClick.AddListener(OnAuthorizedButtonClick);

            DisableAll();
        }

        public void Dispose()
        {
            _closeButton.onClick.RemoveListener(DisableAll);
            _openButton.onClick.RemoveListener(OnOpenButtonClick);
            _authorizedButton.onClick.RemoveListener(OnAuthorizedButtonClick);
            
        }
        
        private void DisableAll()
        {
            CanvasGroupUtil.Disable(_myCanvasGroup);
            CanvasGroupUtil.Disable(_authorizedCanvasGroup);
            CanvasGroupUtil.Disable(_baseCanvasGroup);
        }
        
        private void OnOpenButtonClick()
        {
            CanvasGroupUtil.Enable(_baseCanvasGroup);
            
#if UNITY_EDITOR == false
            if (PlayerAccount.IsAuthorized)
            {
                CanvasGroupUtil.Enable(_myCanvasGroup);
                Opened?.Invoke();
            }
            else
            {
                CanvasGroupUtil.Enable(_authorizedCanvasGroup);
            }
            return;
#endif
            
            Opened?.Invoke();
            CanvasGroupUtil.Enable(_myCanvasGroup);
        }

        private void OnAuthorizedButtonClick()
        {
            Authorized?.Invoke();
            DisableAll();
        }
    }
}