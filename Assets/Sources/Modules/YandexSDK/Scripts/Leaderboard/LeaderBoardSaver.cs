using System;
using Agava.YandexGames;
using Sources.Modules.Level.Interfaces;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts.Leaderboard
{
    public class LeaderBoardSaver : IDisposable
    {
        private readonly ILevelHandlerEvent _levelHandlerEvent;

        public LeaderBoardSaver(ILevelHandlerEvent levelHandlerEvent)
        {
            _levelHandlerEvent = levelHandlerEvent;
            _levelHandlerEvent.LevelUpdated += TrySaveLevelLimit;
        }
        
        public void Dispose()
        {
            _levelHandlerEvent.LevelUpdated -= TrySaveLevelLimit;
        }
        
        private void TrySaveLevelLimit(int level)
        {
#if UNITY_EDITOR
            return;
#endif
            
            if (PlayerAccount.IsAuthorized == false)
                return;
            
            Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardStrings.LeaderboardName, (result) =>
            {
                if (result == null || result.score < level)
                {
                    Agava.YandexGames.Leaderboard.SetScore(LeaderboardStrings.LeaderboardName, level);
                }
            });
        }
    }
}