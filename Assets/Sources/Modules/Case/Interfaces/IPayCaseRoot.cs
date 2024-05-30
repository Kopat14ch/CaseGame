using System;

namespace Sources.Modules.Case.Interfaces
{
    public interface IPayCaseRoot
    {
        public bool IsPurchase { get; }

        public event Action PurchaseUpdated;
    }
}