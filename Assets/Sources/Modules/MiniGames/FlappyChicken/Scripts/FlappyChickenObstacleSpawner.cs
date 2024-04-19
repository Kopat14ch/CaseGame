using System;
using System.Linq;
using Sources.Modules.MiniGames.FlappyChicken.Interfaces;
using UnityEngine;
using Random = System.Random;

namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    public class FlappyChickenObstacleSpawner : IDisposable
    {
        private readonly FlappyChickenSpawnPoint _flappyChickenSpawnPoint;
        private readonly IFlappyChickenView _flappyChickenView;
        private readonly FlappyChickenObstacleDisabler _disabler;
        private readonly Vector3 _offsetPosition;
        private readonly Random _random;
        
        private int _currentLastIndex;
        
        private FlappyChickenObstacleRoot[] _flappyChickenObstacles;
        
        public FlappyChickenObstacleSpawner(FlappyChickenObstacleRoots obstacles, FlappyChickenSpawnPoint flappyChickenSpawnPoint, IFlappyChickenView flappyChickenView, FlappyChickenObstacleDisabler disabler)
        {
            _offsetPosition = new Vector3(260, 0, 0);
            _random = new Random();
            
            _flappyChickenObstacles = obstacles.Obstacles;
            _flappyChickenSpawnPoint = flappyChickenSpawnPoint;
            _flappyChickenView = flappyChickenView;
            _disabler = disabler;
            _currentLastIndex = _flappyChickenObstacles.Length - 1;

            _disabler.DisableEntered += OnDisableEntered;

            _flappyChickenView.EnterButtonClick += Init;
        }
        
        public void Dispose()
        {
            _disabler.DisableEntered += OnDisableEntered;
            _flappyChickenView.EnterButtonClick -= Init;
        }
        
        private void Init()
        {
            _flappyChickenObstacles = _flappyChickenObstacles.OrderBy(_ => _random.Next()).ToArray();
            
            _flappyChickenObstacles.First().SetPosition( _flappyChickenSpawnPoint.LocalPosition);
            
            for (int i = 1; i < _flappyChickenObstacles.Length; i++)
                _flappyChickenObstacles[i].SetPosition(_flappyChickenObstacles[i-1].LocalPosition + _offsetPosition);
        }

        private void OnDisableEntered(FlappyChickenObstacleRoot obstacle)
        {
            Spawn(obstacle);

            _currentLastIndex %= _flappyChickenObstacles.Length - 1;
        }

        private void Spawn(FlappyChickenObstacleRoot obstacle)
        {
            obstacle.SetPosition(_flappyChickenObstacles[_currentLastIndex].LocalPosition);
        }
    }
}