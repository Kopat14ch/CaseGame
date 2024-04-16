using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    public class FlappyChickenHandler : IDisposable
    {
        private const float Force = 170;
            
        private readonly PlayerInput _player;
        
        private Rigidbody2D _rigidbody2D;

        public FlappyChickenHandler(FlappyChickenRoot flappyChickenRoot)
        {
            _rigidbody2D = flappyChickenRoot.MyRigidBody;
            _player = new PlayerInput();
            _player.Enable();
            _player.FlappyChicken.Jump.performed += OnJump;
        }

        public void Dispose()
        {
            _player.FlappyChicken.Jump.performed -= OnJump;
            _player?.Dispose();
        }
        
        private void OnJump(InputAction.CallbackContext ctx)
        {
            _rigidbody2D.AddForce(Vector2.up * Force, ForceMode2D.Impulse);
        }
    }
}
