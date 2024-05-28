using System;
using Sources.Modules.Case.Scripts;

namespace Sources.Modules.CaseOpener.Interfaces
{
    public interface ICaseOpener
    {
        public void Open(CaseData caseData, bool again = false, Action onScrollComplete = null);
    }
}