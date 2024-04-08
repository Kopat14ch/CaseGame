using System;

namespace Sources.Modules.Common.Interfaces
{
    public interface ISpeedUpdater
    {
        public event Action<float> SpeedUpdated; 
    }
}