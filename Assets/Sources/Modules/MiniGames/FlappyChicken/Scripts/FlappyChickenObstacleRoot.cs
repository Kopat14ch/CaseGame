using System;
using UnityEngine;

namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    public class FlappyChickenObstacleRoot : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Vector2 _veclocity;
        
        public Vector3 LocalPosition => transform.localPosition;
        
        public event Action<FlappyChickenObstacleRoot> DisableEntered;
        
        public FlappyChickenObstacle[] FlappyChickenObstacles => GetComponentsInChildren<FlappyChickenObstacle>();
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _veclocity = new(-65,0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<FlappyChickenObstacleDisabler>() != null)
            {
                DisableEntered?.Invoke(this);
            }
        }

        public void SetPosition(Vector3 position)
        {
            transform.localPosition = position;
            TryMove();
        }

        public void Stop()
        {
            if (_rigidbody2D == null)
                return;
            
            _rigidbody2D.velocity = Vector2.zero;
        }

        private void TryMove()
        {
            if (_rigidbody2D == null)
                return;

            _rigidbody2D.velocity = _veclocity;
        }
    }
}