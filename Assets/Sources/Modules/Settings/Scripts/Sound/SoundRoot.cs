using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.Settings.Scripts.Sound
{
    public class SoundRoot : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _toggleButton;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _enableSprite;
        [SerializeField] private Sprite _disableSprite;

        private SoundSettingsHandler _soundSettingsHandler;

        [Inject]
        public void Construct()
        {
            _soundSettingsHandler = new SoundSettingsHandler(_slider, _toggleButton, _image, _enableSprite, _disableSprite);
        }

        private void OnEnable()
        {
            _soundSettingsHandler.Enable();
        }

        private void OnDisable()
        {
            _soundSettingsHandler.Disable();
        }
    }
}
