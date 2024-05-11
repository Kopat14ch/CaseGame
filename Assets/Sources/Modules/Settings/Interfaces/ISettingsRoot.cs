using System;
using Sources.Modules.Settings.Scripts.Sound;

namespace Sources.Modules.Settings.Interfaces
{
    public interface ISettingsRoot
    {
        public event Action<SoundSettingsHandler> Disabled;
    }
}