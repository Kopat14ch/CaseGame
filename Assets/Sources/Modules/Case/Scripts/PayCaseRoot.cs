using System;
using Agava.YandexGames;
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

        public event Action PurchaseUpdated;
        public event Action<string,string,bool> PurchaseDataUpdated;

        public override void Construct(ICaseOpener caseOpenerRoot, IWalletRoot walletRoot)
        {
            _payCaseView.Init(this);
            base.Construct(caseOpenerRoot, walletRoot);
        }

        private async void Awake()
        {
            await YandexSaves.Instance.IsLoadedAsync();

            PayCaseData[] payCaseDatas = YandexSaves.Instance.Load().PayCaseDatas;

            foreach (var payCaseData in payCaseDatas)
            {
                if (payCaseData.Id != _id) 
                    continue;
                
                IsPurchase = payCaseData.IsPurchase;
                _purchaseToken = payCaseData.PurchaseToken;
                break;
            }
            
            PurchaseUpdated?.Invoke();
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
                    Billing.ConsumeProduct(_purchaseToken, () =>
                    {
                        IsPurchase = false;
                        _purchaseToken = null;
                        PurchaseUpdated?.Invoke();
                        PurchaseDataUpdated?.Invoke(_id, _purchaseToken, IsPurchase);
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
                    PurchaseDataUpdated?.Invoke(_id, _purchaseToken, IsPurchase);
                });
            }
        }
    }
}