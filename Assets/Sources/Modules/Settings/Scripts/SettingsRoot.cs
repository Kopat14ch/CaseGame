using System;
using Sources.Modules.Settings.Interfaces;
using Sources.Modules.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Settings.Scripts
{
    public class SettingsRoot : MonoBehaviour, ISettingsRoot
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _enableButton;
        [SerializeField] private Button _disableButton;
        
        public event Action Disabled;

        private void Awake()
        {
            CanvasGroupUtil.Disable(_canvasGroup);
        }

        private void OnEnable()
        {
            _enableButton.onClick.AddListener(OnEnableButtonClick);
            _disableButton.onClick.AddListener(OnDisableButtonClick);
        }

        private void OnDisable()
        {
            _enableButton.onClick.RemoveListener(OnEnableButtonClick);
            _disableButton.onClick.RemoveListener(OnDisableButtonClick);
        }

        private void OnDisableButtonClick()
        {
            CanvasGroupUtil.Disable(_canvasGroup);
            Disabled?.Invoke();
        }

        private void OnEnableButtonClick()
        {
            CanvasGroupUtil.Enable(_canvasGroup);
        }
    }
}