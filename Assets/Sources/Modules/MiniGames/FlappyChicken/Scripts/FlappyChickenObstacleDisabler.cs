using System;
using UnityEngine;

namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    public class FlappyChickenObstacleDisabler : MonoBehaviour
    {
        public event Action<FlappyChickenObstacleRoot> DisableEntered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out FlappyChickenObstacleRoot flappyChickenObstacleRoot))
                DisableEntered?.Invoke(flappyChickenObstacleRoot);
        }
    }
}