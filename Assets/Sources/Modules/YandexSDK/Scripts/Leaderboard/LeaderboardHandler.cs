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
        
        private readonly List<UserRoot> _userRoots;

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

        private UserRoot ConstructPlayer(int top, string nickName, int level, Color color)
        {
            UserRoot userRoot = Object.Instantiate(_userPrefab, _container.transform);
            userRoot.Init(top, nickName, level, color);
            
            return userRoot;
        }

        private void OnOpened()
        {
#if UNITY_EDITOR
            _container.Clear();
            _userRoots.Clear();
            
            for (int i = 0; i < 10; i++)
            {
                ColorWithRank.GetColor(i + 1, out Color color);
                _userRoots.Add(ConstructPlayer(i + 1, $"Бобер {i + 1}", i, color));
            }
            
            ConstructLeaderboard();
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
                
                ColorWithRank.GetColor(result.rank, out Color color);
                
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

                    ColorWithRank.GetColor(rank, out Color color);
                    
                    _userRoots.Add(ConstructPlayer(rank, name, level, color));
                }
            });
            
            ConstructLeaderboard();
        }

        private void ConstructLeaderboard()
        {
            /*_userRoots = _userRoots.OrderBy(root => root.Level).ToList();
            
            foreach (var root in _userRoots)
                root.SetParent(_container.transform);*/
        }
    }

    static class ColorWithRank
    {
        private static readonly Color[] TopPlayerColors =
        {
            new (246f / 255f, 255f / 255f, 0f / 255f),    // Яркий желтый
            new (125f / 255f, 125f / 255f, 125f / 255f),  // Серый
            new (135f / 255f, 98f / 255f, 41f / 255f)     // Коричневый
        };
        
        private static readonly IReadOnlyDictionary<int, Color> Colors = new Dictionary<int, Color>()
        {
            {1, TopPlayerColors[0]},
            {2, TopPlayerColors[1]},
            {3, TopPlayerColors[2]}
        };
        
        public static void GetColor(int rank, out Color resultColor)
        {
            resultColor = Colors.TryGetValue(rank, out Color color) ?
                resultColor = color :
                resultColor = Color.white;
        }
    }
}
