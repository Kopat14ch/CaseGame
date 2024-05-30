using System;
using Sources.Modules.MiniGames.Clicker.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.MiniGames.Clicker.Scripts
{
    [RequireComponent(typeof(Button))]
    public class CoinRoot : MonoBehaviour, ICoinRoot
    {
        private Button _button;
        
        public event Action Clicked;
        public event Action Closed;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }


        private void OnClicked()
        {
            Clicked?.Invoke();
        }
    }
}