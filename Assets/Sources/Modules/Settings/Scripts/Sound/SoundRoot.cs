using UnityEngine;
using UnityEngine.Serialization;
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

        public Slider Slider => _slider;
        public Button ToggleButton => _toggleButton;
        public Image Image => _image;
        public Sprite EnableSprite => _enableSprite;
        public Sprite DisableSprite => _disableSprite;

        public SoundSettingsHandler SoundSettingsHandler { get; private set; }

        [Inject]
        public void Construct(SoundSettingsHandler soundSettingsHandler)
        {
            SoundSettingsHandler = soundSettingsHandler;
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
