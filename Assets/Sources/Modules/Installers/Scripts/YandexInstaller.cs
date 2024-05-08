using Sources.Modules.YandexSDK.Scripts;
using Sources.Modules.YandexSDK.Scripts.Leaderboard;
using Sources.Modules.YandexSDK.Scripts.Leaderboard.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Installers.Scripts
{
    public class YandexInstaller : MonoInstaller
    {
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private YandexSDKRoot _yandexSDKRoot;
        [SerializeField] private UserRoot _userPrefab;
        [SerializeField] private UsersContainer _usersContainer;
        
        public override void InstallBindings()
        {
            BindSaver();
            BindLeaderboard();
        }

        private void BindLeaderboard()
        {
            Container.Bind<LeaderBoardSaver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LeaderboardView>().FromInstance(_leaderboardView).AsSingle();
            Container.BindInterfacesTo<LeaderboardViewHandler>().AsSingle().NonLazy();

            LeaderboardHandler leaderboardHandler = new LeaderboardHandler(_userPrefab, Container.Resolve<ILeaderboardViewHandler>(), _usersContainer);
            Container.Bind<LeaderboardHandler>().FromInstance(leaderboardHandler).AsSingle().NonLazy();
        }

        private void BindSaver()
        {
            Container.Bind<YandexSaves>().AsSingle().NonLazy();
        }
    }
}