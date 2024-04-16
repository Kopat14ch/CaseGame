using System;
using Sources.Modules.Utils;
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
            CanvasGroupUtil.Enable(_currentCanvasGroup);
        }

        public void DisableUI()
        {
            CanvasGroupUtil.Disable(_currentCanvasGroup);
        }

        private void OnButtonClick()
        {
            Clicked?.Invoke(this);
        }
    }
}