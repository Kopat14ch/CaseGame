using System;
using System.Collections.Generic;
using System.Linq;
using Agava.YandexGames;
using Sources.Modules.YandexSDK.Scripts.Leaderboard.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Modules.YandexSDK.Scripts.Leaderboard
{
    public class LeaderboardHandler : IDisposable
    {
        private const string AnonymousName = "Anonymous";
        private const int MaxPlayersCount = 10;
        private const int MinPlayersCount = 0;
        
        private readonly UserRoot _userPrefab;
        private readonly ILeaderboardViewHandler _leaderboardViewHandler;
        private readonly UsersContainer _container;
        
        private List<UserRoot> _userRoots;

        public LeaderboardHandler(UserRoot userPrefab, ILeaderboardViewHandler leaderboardViewHandler, UsersContainer container)
        {
            _userRoots = new List<UserRoot>();
            _userPrefab = userPrefab;
            _leaderboardViewHandler = leaderboardViewHandler;
            _container = container;

            _leaderboardViewHandler.Opened += OnOpened;
        }
        
        public void Dispose()
        {
            _leaderboardViewHandler.Opened -= OnOpened;
        }

        private UserRoot ConstructPlayer(int top, string nickName, int level, Color? color = null)
        {
            UserRoot userRoot = Object.Instantiate(_userPrefab);
            userRoot.Init(top,nickName, level, color);
            
            return userRoot;
        }

        private void OnOpened()
        {
#if UNITY_EDITOR
            return;
#endif
            if (PlayerAccount.IsAuthorized == false)
                return;

            if (_userRoots.Count > 0)
            {
                foreach (var userRoot in _userRoots)
                    Object.Destroy(userRoot.gameObject);
                
                _userRoots.Clear();
            }
            
            Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardStrings.LeaderboardName, (result) =>
            {
                int rank = result.rank;
                int level = result.score;
                string name = "Я";
                
                ColorWithRank.Colors.TryGetValue(result.rank, out Color color);
                
                _userRoots.Add(ConstructPlayer(rank, name, level, color));
            });
            
            Agava.YandexGames.Leaderboard.GetEntries(LeaderboardStrings.LeaderboardName, (result) =>
            {
                int entriesClamp = Mathf.Clamp(result.entries.Length, MinPlayersCount, MaxPlayersCount);
                LeaderboardEntryResponse[] entries = result.entries;
                
                for (int i = 0; i < entriesClamp; i++)
                {
                    int rank = entries[i].rank;
                    int level = entries[i].score;
                    string name = entries[i].player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = AnonymousName;

                    ColorWithRank.Colors.TryGetValue(rank, out Color color);
                    
                    _userRoots.Add(ConstructPlayer(rank, name, level, color));
                }
            });
            
            ConstructLeaderboard();
        }

        private void ConstructLeaderboard()
        {
            _userRoots = _userRoots.OrderBy(root => root.Level).ToList();
            
            foreach (var root in _userRoots)
                root.SetParent(_container.transform);
        }
    }

    static class ColorWithRank
    {
        public static readonly IReadOnlyDictionary<int, Color> Colors = new Dictionary<int, Color>()
        {
            {1, new Color(246,255,0)},
            {2, new Color(125,125,125)},
            {3, new Color(135,98,41)}
        };
    }
}
