using System;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Sources.Modules.Case.Interfaces;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Wallet.Interfaces;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Sources.Modules.Case.Scripts
{
    public class PayCaseRoot : CaseRoot, IPayCaseRoot
    {
        [SerializeField] private string _id;
        [SerializeField] private PayCaseView _payCaseView;
        
        public bool IsPurchase { get; private set; }
        private string _purchaseToken;


        public string Id => _id;
        public event Action PurchaseUpdated;

        public override void Construct(ICaseOpener caseOpenerRoot, IWalletRoot walletRoot)
        {
            _payCaseView.Init(this);
            base.Construct(caseOpenerRoot, walletRoot);
        }


        protected override void OnOpenButtonClicked()
        {
#if UNITY_EDITOR
            CaseOpenerRoot.Open(Data);
            return;
#endif
            
            if (IsPurchase && _purchaseToken != null)
            {
                CaseOpenerRoot.Open(Data, onScrollComplete: () =>
                {
                    Billing.ConsumeProduct(_purchaseToken, async () =>
                    {
                        PurchasedProduct purchasedProduct = await TryGetPurchasedProductAsync();
                        
                        if (purchasedProduct != null)
                        {
                            IsPurchase = true;
                            _purchaseToken = purchasedProduct.purchaseToken;
                        }
                        else
                        {
                            IsPurchase = false;
                            _purchaseToken = null;
                        }
     
                        PurchaseUpdated?.Invoke();
                    });
                });
            }
            else
            {
                
                Billing.PurchaseProduct(_id, purchaseProductResponse =>
                {
                    _purchaseToken = purchaseProductResponse.purchaseData.purchaseToken;
                    IsPurchase = true;
                    PurchaseUpdated?.Invoke();
                });
            }
        }

        private async UniTask<PurchasedProduct> TryGetPurchasedProductAsync()
        {
            var tcs = new UniTaskCompletionSource<PurchasedProduct>();

            Billing.GetPurchasedProducts(
                onSuccessCallback: response =>
                {
                    PurchasedProduct purchasedProduct = null;
                    foreach (var product in response.purchasedProducts)
                    {
                        if (product.productID != _id)
                            continue;

                        purchasedProduct = product;
                        break;
                    }

                    tcs.TrySetResult(purchasedProduct);
                });
            return await tcs.Task;
        }

        public void SetHavePurchased(string purchaseToken)
        {
            IsPurchase = true;
            _purchaseToken = purchaseToken;
            PurchaseUpdated?.Invoke();
        }

    }
}