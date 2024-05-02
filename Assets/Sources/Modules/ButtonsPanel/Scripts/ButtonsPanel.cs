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

        private Color _selectColor;
        private Color _normalColor;
        private ColorBlock _colorBlock;
        
        public event Action<ButtonsPanel> Clicked;


        public void Init()
        {
            _colorBlock = _currentButton.colors;
            _normalColor = _colorBlock.normalColor;
            _selectColor = _colorBlock.selectedColor;
        }
        
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
            _colorBlock.normalColor = _selectColor;
            _currentButton.colors = _colorBlock;
        }

        public void DisableUI()
        {
            _colorBlock.normalColor = _normalColor;
            _currentButton.colors = _colorBlock;
            
            CanvasGroupUtil.Disable(_currentCanvasGroup);
        }

        private void OnButtonClick()
        {
            Clicked?.Invoke(this);
        }
    }
}