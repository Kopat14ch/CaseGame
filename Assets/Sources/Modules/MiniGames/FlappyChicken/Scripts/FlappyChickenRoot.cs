using System;
using Sources.Modules.Wallet.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FlappyChickenRoot : MonoBehaviour
    {
        [SerializeField] private FlappyChickenView _flappyChickenView;
        
        private const int AddEnterValue = 5;
        
        private Rigidbody2D _rigidbody2D;
        private IWalletRoot _walletRoot;
        private Vector3 _startPosition;

        public event Action<int> CoinAdded;
        public event Action Disabled;

        public Rigidbody2D MyRigidBody => _rigidbody2D;

        private FlappyChickenHandler _flappyChickenHandler;


        [Inject]
        public void Construct(IWalletRoot walletRoot)
        {
            _walletRoot = walletRoot;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _flappyChickenHandler = new FlappyChickenHandler(this);
        }

        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            _flappyChickenView.EnterButtonClick += Enable;
            _flappyChickenView.Disable += Disable;
        }

        private void OnDisable()
        {
            _flappyChickenView.EnterButtonClick -= Enable;
            _flappyChickenView.Disable -= Disable;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<FlappyChickenCoinCollider>() != null)
            {
                _walletRoot.AddMoney(AddEnterValue);
                CoinAdded?.Invoke(AddEnterValue);
            }

            if (other.GetComponent<FlappyChickenObstacle>() != null)
            {
                Disable();
                Disabled?.Invoke();
            }
        }
        
        private void Disable()
        {
            _rigidbody2D.isKinematic = true;
            _rigidbody2D.simulated = false;
        }

        private void Enable()
        {
            transform.localPosition = _startPosition;
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.simulated = true;
        }
    }
}
