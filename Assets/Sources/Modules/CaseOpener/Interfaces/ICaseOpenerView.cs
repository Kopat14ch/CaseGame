using System;

namespace Sources.Modules.CaseOpener.Interfaces
{
    public interface ICaseOpenerView
    {
        public event Action SellButtonClicked;
        public event Action OpenAgainButtonClicked;
    }
}