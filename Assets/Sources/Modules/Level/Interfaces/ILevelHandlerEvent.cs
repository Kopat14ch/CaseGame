using System;

namespace Sources.Modules.Level.Interfaces
{
    public interface ILevelHandlerEvent
    {
        public event Action<uint> ExperienceUpdated;
        public event Action<int, uint> LevelLimitUpdated;
        public event Action<int> LevelUpdated; 
    }
}