using System;

namespace Sources.Modules.Level.Interfaces
{
    public interface ILevelHandlerEvent
    {
        public event Action<uint> ExperienceUpdated;
        public event Action<uint, uint> LevelUpdated; 
    }
}