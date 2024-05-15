using Sources.Modules.MiniGames.Clicker.Scripts;
using Sources.Modules.MiniGames.FlappyChicken.Scripts;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Installers.Scripts
{
    public class MiniGamesInstaller : MonoInstaller
    {
        [SerializeField] private CoinRoot coinRoot;
        [SerializeField] private FlappyChickenRoot _flappyChickenRoot;
        [SerializeField] private FlappyChickenObstacleRoots _flappyChickenObstacles;
        [SerializeField] private FlappyChickenSpawnPoint _flappyChickenSpawnPoint;
        [SerializeField] private FlappyChickenView _flappyChickenView;
        [SerializeField] private FlappyChickenObstacleDisabler _flappyChickenObstacleDisabler;
        
        public override void InstallBindings()
        {
            BindClicker();
            BindFlappyChicken();
        }
        
        
        private void BindClicker()
        {
            Container.Bind<CoinRoot>().FromInstance(coinRoot).AsSingle();
            Container.BindInterfacesTo<CoinRoot>().FromResolve();
            Container.Bind<ClickerHandler>().AsSingle().NonLazy();
        }

        private void BindFlappyChicken()
        {
            Container.Bind<FlappyChickenObstacleDisabler>().FromInstance(_flappyChickenObstacleDisabler).AsSingle();
            Container.Bind<FlappyChickenRoot>().FromInstance(_flappyChickenRoot).AsSingle();
            Container.Bind<FlappyChickenObstacleRoots>().FromInstance(_flappyChickenObstacles).AsSingle();
            Container.Bind<FlappyChickenSpawnPoint>().FromInstance(_flappyChickenSpawnPoint).AsSingle();
            Container.Bind<FlappyChickenObstacleSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FlappyChickenView>().FromInstance(_flappyChickenView).AsSingle();
        }
    }
}