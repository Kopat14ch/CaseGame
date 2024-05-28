using System;

namespace Sources.Modules.Case.Scripts
{
    [Serializable]
    public class PayCaseData
    {
        public string Id;
        public bool IsPurchase;
        public string PurchaseToken;
    }
}