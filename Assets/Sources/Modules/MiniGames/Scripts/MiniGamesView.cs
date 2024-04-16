using Sources.Modules.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.MiniGames.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class MiniGamesView : MonoBehaviour
    {
        [SerializeField] private Button _enterButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private CanvasGroup _leftPanelButtonsCanvasGroup;
        [SerializeField] private CanvasGroup _miniGamesButtons;
        
        private CanvasGroup _gameplayCanvasGroup;
        
        private void Awake()
        {
            _exitButton.gameObject.SetActive(false);
            _gameplayCanvasGroup = GetComponent<CanvasGroup>();
        }

        protected virtual void OnEnable()
        {
            _enterButton.onClick.AddListener(OnEnterButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        protected virtual void OnDisable()
        {
            _enterButton.onClick.RemoveListener(OnEnterButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        protected virtual void OnEnterButtonClick()
        {
            CanvasGroupUtil.Enable(_gameplayCanvasGroup);
            CanvasGroupUtil.Disable(_leftPanelButtonsCanvasGroup);
            CanvasGroupUtil.Disable(_miniGamesButtons);
            
            _exitButton.gameObject.SetActive(true);
        }

        protected virtual void OnExitButtonClick()
        {
            CanvasGroupUtil.Disable(_gameplayCanvasGroup);
            CanvasGroupUtil.Enable(_leftPanelButtonsCanvasGroup);
            CanvasGroupUtil.Enable(_miniGamesButtons);
            
            _exitButton.gameObject.SetActive(false);
        }
    }
}