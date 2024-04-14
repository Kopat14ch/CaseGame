using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Wallet.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Case.Scripts
{
    [RequireComponent(typeof(CaseView))]
    public class CaseRoot : MonoBehaviour
    {
        [SerializeField] private CaseData _data;
        
        private CaseView _caseView;
        private ICaseOpener _caseOpenerRoot;
        private IWalletRoot _walletRoot;

        [Inject]
        public void Construct(ICaseOpener caseOpenerRoot, IWalletRoot walletRoot)
        {
            _walletRoot = walletRoot;
            _caseOpenerRoot = caseOpenerRoot;
            _caseView = GetComponent<CaseView>();
            _caseView.Init(_data);
        }
        
        private void OnEnable()
        {
            _caseView.OpenButtonClicked += OnOpenButtonClicked;
        }

        private void OnDisable()
        {
            _caseView.OpenButtonClicked -= OnOpenButtonClicked;
        }

        private void OnOpenButtonClicked()
        {
            if (_walletRoot.TryTakeMoney(_data.Price) == false)
                return;
            
            _caseOpenerRoot.Open(_data);
        }
        
    }
}
