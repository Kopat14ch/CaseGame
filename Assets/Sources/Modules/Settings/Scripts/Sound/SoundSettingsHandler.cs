using Cysharp.Threading.Tasks;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Settings.Scripts.Sound
{
    public class SoundSettingsHandler
    {
        private readonly Slider _slider;
        private readonly Button _toggleButton;
        private readonly Image _image;
        private readonly Sprite _enableSprite;
        private readonly Sprite _disableSprite;
        private float _lastVolume;
        
        private bool _isEnable;

        public SoundSettingsHandler(Slider slider, Button toggleButton, Image image, Sprite enableSprite, Sprite disableSprite)
        {
            _slider = slider;
            _toggleButton = toggleButton;
            _image = image;
            _enableSprite = enableSprite;
            _disableSprite = disableSprite;
            
            Init();
        }

        public void Enable()
        {
            _toggleButton.onClick.AddListener(() => Toggle());
            _slider.onValueChanged.AddListener(OnSliderUpdate);
        }

        public void Disable()
        {
            _toggleButton.onClick.RemoveListener(() => Toggle());
            _slider.onValueChanged.RemoveListener(OnSliderUpdate);
        }
        
        private async void Init()
        {
            await UniTask.WaitUntil(() => YandexSaves.Instance.IsLoaded);

            SoundData soundData = YandexSaves.Instance.Load().SoundData;

            _isEnable = soundData.IsEnable;
            _lastVolume = soundData.LastVolume;
            
            _slider.value = _lastVolume;
            
            AudioListener.volume = _lastVolume;
            
            UpdateSprite(_isEnable);
        }
        


        private void Toggle(float volume = -1)
        {
            _isEnable = !_isEnable;
            
            UpdateSprite(_isEnable);

            if (volume >= 0)
                _lastVolume = volume;
            
            if (_isEnable)
            {
                AudioListener.volume = _lastVolume;
            }
            else
            {
                _lastVolume = AudioListener.volume;
                AudioListener.volume = 0;
            }
            
        }

        private void UpdateSprite(bool isEnable)
        {
            _image.sprite = isEnable ? _enableSprite : _disableSprite;
        }

        private void OnSliderUpdate(float value)
        {
            if (value <= 0 && _isEnable)
                Toggle(value);
            else if (value > 0 && _isEnable == false)
                Toggle(value);
            else
                AudioListener.volume = value;
        }

    }
}