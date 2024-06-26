﻿using Agava.YandexGames;
using Sources.Modules.YandexSDK.Interfaces;
using Sources.Modules.YandexSDK.Scripts.Leaderboard.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexSDKRoot : MonoBehaviour, IYandexSDKRoot
    {
        private ILeaderboardViewHandler _leaderboardViewHandler;

        public bool IsAuthorized
        {
            get
            {
#if UNITY_EDITOR
                return true;
#endif
                
                return PlayerAccount.IsAuthorized;
            }
        }

        [Inject]
        public void Construct(ILeaderboardViewHandler leaderboardViewHandler)
        {
#if UNITY_EDITOR == false
            YandexGamesSdk.GameReady();
#endif
            
            _leaderboardViewHandler = leaderboardViewHandler;
        }

        private void OnEnable()
        {
            _leaderboardViewHandler.Authorized += OnLeaderboardAuthorized;
        }

        private void OnDisable()
        {
            _leaderboardViewHandler.Authorized -= OnLeaderboardAuthorized;
        }

        private void OnLeaderboardAuthorized()
        {
#if UNITY_EDITOR
            return;
#endif
            
            PlayerAccount.Authorize();

            if (PlayerAccount.IsAuthorized)
                PlayerAccount.RequestPersonalProfileDataPermission();
        }
    }
}