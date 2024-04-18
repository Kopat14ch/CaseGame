using System;
using UnityEngine;

namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    public class FlappyChickenObstacleRoot : MonoBehaviour
    {
        
        private FlappyChickenObstacleHandler _handler;
        
        public Vector3 LocalPosition => transform.localPosition;
        
        private void Awake()
        {
            _handler = new FlappyChickenObstacleHandler(GetComponent<Rigidbody2D>());
        }

        public void SetPosition(Vector3 position)
        {
            _handler.SetPosition(position);
        }

        public void Stop()
        {
            _handler.Stop();
        }
    }
}