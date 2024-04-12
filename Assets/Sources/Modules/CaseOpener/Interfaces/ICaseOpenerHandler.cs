using System;

namespace Sources.Modules.CaseOpener.Interfaces
{
    public interface ICaseOpenerHandler
    {
        public event Action<Common.Scripts.Weapon> ScrollComplete;
    }
}