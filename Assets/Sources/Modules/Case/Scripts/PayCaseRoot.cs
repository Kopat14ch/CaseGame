using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.Case.Scripts
{
    public class PayCaseRoot : CaseRoot
    {
        [SerializeField] private string _id;
        
        protected override void OnOpenButtonClicked()
        {
#if UNITY_EDITOR
            CaseOpenerRoot.Open(Data);
            return;
#endif
            
            Billing.PurchaseProduct(_id, purchaseProductResponse =>
            {
                Billing.ConsumeProduct(purchaseProductResponse.purchaseData.purchaseToken, () =>
                {
                    CaseOpenerRoot.Open(Data);
                });
            });
        }
    }
}