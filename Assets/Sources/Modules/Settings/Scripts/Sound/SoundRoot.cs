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

        public SoundSettingsHandler SoundSettingsHandler { get; private set; }

        [Inject]
        public void Construct()
        {
            SoundSettingsHandler = new SoundSettingsHandler(_slider, _toggleButton, _image, _enableSprite, _disableSprite);
        }

        private void OnEnable()
        {
            SoundSettingsHandler.Enable();
        }

        private void OnDisable()
        {
            SoundSettingsHandler.Disable();
        }
    }
}
