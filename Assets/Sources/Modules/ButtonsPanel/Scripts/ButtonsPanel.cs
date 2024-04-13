using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.ButtonsPanel.Scripts
{
    [Serializable]
    public class ButtonsPanel
    {
        [SerializeField] private Button _currentButton;
        [SerializeField] private CanvasGroup _currentCanvasGroup;

        public event Action<ButtonsPanel> Clicked;
        
        public void Enable()
        {
            _currentButton.onClick.AddListener(OnButtonClick);
        }

        public void Disable()
        {
            _currentButton.onClick.RemoveListener(OnButtonClick);
        }
        

        public void EnableUI()
        {
            _currentCanvasGroup.alpha = 1;
            _currentCanvasGroup.interactable = true;
            _currentCanvasGroup.blocksRaycasts = true;
        }

        public void DisableUI()
        {
            _currentCanvasGroup.alpha = 0;
            _currentCanvasGroup.interactable = false;
            _currentCanvasGroup.blocksRaycasts = false;
        }

        private void OnButtonClick()
        {
            Clicked?.Invoke(this);
        }
    }
}