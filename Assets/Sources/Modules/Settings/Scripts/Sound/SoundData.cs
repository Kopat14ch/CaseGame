using System;

namespace Sources.Modules.Settings.Scripts.Sound
{
    [Serializable]
    public class SoundData
    {
        public float LastVolume;
        public bool IsEnable;

        public SoundData()
        {
            LastVolume = 1f;
            IsEnable = true;
        }
    }
}