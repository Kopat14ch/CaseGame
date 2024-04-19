using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    public class FlappyChickenObstacleHandler : IDisposable
    {
        private readonly Rigidbody2D _rigidbody2D;
        private float _speed;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isMoving;

        public FlappyChickenObstacleHandler(Rigidbody2D rigidbody2D)
        {
            _speed = 5200;
            _rigidbody2D = rigidbody2D;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void SetPosition(Vector3 position)
        {
            _rigidbody2D.transform.localPosition = position;
            
            if (_isMoving == false)
                Move(_cancellationTokenSource.Token).Forget();
        }

        public void Stop()
        {
            if (_rigidbody2D == null)
                return;

            _rigidbody2D.velocity = Vector2.zero;
            _cancellationTokenSource.Cancel();
        }
        
        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        private async UniTask Move(CancellationToken cancellationToken)
        {
            _isMoving = true;
            
            try
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    _rigidbody2D.velocity = _speed * Time.fixedDeltaTime * Vector2.left;
                    
                    await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);
                }
            }
            finally
            {
                _isMoving = false;
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }
    }
}