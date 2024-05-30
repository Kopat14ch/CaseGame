using System;
using Agava.WebUtility;
using Sources.Modules.Settings.Interfaces;
using UnityEngine;

namespace Sources.Modules.Background.Scripts
{
    public class BackgroundHandler : IDisposable
    {
        private readonly ISoundSettingsHandler _soundSettingsHandler;

        public BackgroundHandler(ISoundSettingsHandler soundSettingsHandler)
        {
            _soundSettingsHandler = soundSettingsHandler;

            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }
        
        public void Dispose()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }
        
        private void OnInBackgroundChange(bool inBackground)
        {
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : _soundSettingsHandler.LastVolume;
        }
    }
}