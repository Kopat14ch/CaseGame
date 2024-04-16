﻿using UnityEngine;
using Zenject;

namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    public class FlappyChickenObstacleRoots : MonoBehaviour
    {
        [field: SerializeField] public FlappyChickenObstacleRoot[] Obstacles { get; private set; }
        
        private FlappyChickenRoot _flappyChickenRoot;


        [Inject]
        public void Construct(FlappyChickenRoot flappyChickenRoot)
        {
            _flappyChickenRoot = flappyChickenRoot;
        }
        
        private void OnEnable()
        {
            _flappyChickenRoot.Disabled += OnPlayerEntered;
        }

        private void OnDisable()
        {
            _flappyChickenRoot.Disabled += OnPlayerEntered;
        }


        private void OnPlayerEntered()
        {
            foreach (var obstacle in Obstacles)
                obstacle.Stop();
        }
    }
}