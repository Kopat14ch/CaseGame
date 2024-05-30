using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.Case.Scripts
{
    public class PayCases : MonoBehaviour
    {
        [SerializeField] private List<PayCaseRoot> _payCaseRoots;
        
        private void Awake()
        {
#if UNITY_EDITOR
            return;
#endif
            
            Billing.GetPurchasedProducts(
                onSuccessCallback: response =>
                {
                    foreach (var payCaseRoot in _payCaseRoots)
                    {
                        foreach (var product in response.purchasedProducts)
                        {
                            if (product.productID != payCaseRoot.Id)
                                continue;
                            
                            payCaseRoot.SetHavePurchased(product.purchaseToken);
                            break;
                        }
                    }
                });
        }
    }
}