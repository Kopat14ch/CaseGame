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
        public float LastVolume { get; private set; }
        
        public bool IsEnable { get; private set; }

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

            IsEnable = soundData.IsEnable;
            LastVolume = soundData.LastVolume;
            
            _slider.value = LastVolume;

            if (IsEnable)
                AudioListener.volume = LastVolume;
            
            UpdateSprite(IsEnable);
        }

        private void Toggle(float volume = -1)
        {
            IsEnable = !IsEnable;
            
            UpdateSprite(IsEnable);

            if (volume >= 0)
                LastVolume = volume;
            
            if (IsEnable)
            {
                AudioListener.volume = LastVolume;
            }
            else
            {
                LastVolume = AudioListener.volume;
                AudioListener.volume = 0;
            }
            
        }

        private void UpdateSprite(bool isEnable)
        {
            _image.sprite = isEnable ? _enableSprite : _disableSprite;
        }

        private void OnSliderUpdate(float value)
        {
            if (value <= 0 && IsEnable)
                Toggle(value);
            else if (value > 0 && IsEnable == false)
                Toggle(value);
            else
                AudioListener.volume = value;
        }

    }
}