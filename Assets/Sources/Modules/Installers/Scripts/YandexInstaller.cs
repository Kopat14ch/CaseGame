using Sources.Modules.YandexSDK.Scripts;
using Sources.Modules.YandexSDK.Scripts.Leaderboard;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Installers.Scripts
{
    public class YandexInstaller : MonoInstaller
    {
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private YandexSDKRoot _yandexSDKRoot;
        
        public override void InstallBindings()
        {
            BindLeaderboard();
        }

        private void BindLeaderboard()
        {
            Container.Bind<LeaderBoardSaver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LeaderboardView>().FromInstance(_leaderboardView).AsSingle();
            Container.BindInterfacesTo<LeaderboardViewHandler>().AsSingle().NonLazy();
        }
    }
}