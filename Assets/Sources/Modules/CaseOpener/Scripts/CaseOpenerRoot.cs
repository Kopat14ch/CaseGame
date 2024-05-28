using System;
using Sources.Modules.Case.Scripts;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Wallet.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    [RequireComponent(typeof(CaseOpenerView))]
    public class CaseOpenerRoot : MonoBehaviour, ICaseOpener
    {
        [SerializeField] private CaseOpenerArrow _caseOpenerArrow;
        
        private CaseOpenerHandler _caseOpenerHandler;
        private CaseOpenerView _caseOpenerView;
        private CaseOpenerContent _contentCaseOpener;
        private CaseData _lastCaseData;
        private IWalletRoot _walletRoot;

        private event Action ScrollComplete;
        
        [Inject]
        public void Construct(CaseOpenerHandler caseOpenerHandler, IWalletRoot walletRoot)
        {
            _walletRoot = walletRoot;
            _caseOpenerHandler = caseOpenerHandler;
            _caseOpenerView = GetComponent<CaseOpenerView>();
        }

        private void Start()
        {
            _caseOpenerView.DisableView();
        }

        private void OnEnable()
        {
            _caseOpenerView.OpenAgainButtonClicked += OpenAgain;
            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
        }

        private void OnDisable()
        {
            _caseOpenerView.OpenAgainButtonClicked -= OpenAgain;
            _caseOpenerHandler.ScrollComplete -= OnScrollComplete;
        }

        public void Open(CaseData caseData, bool again = false, Action onScrollComplete = null)
        {
            if (again == false)
                _lastCaseData = caseData;
            else
                _caseOpenerView.DisableView();
            
            _caseOpenerHandler.Open(_lastCaseData.Weapons, _caseOpenerArrow, _caseOpenerView.Content.transform);
            _caseOpenerView.EnableView(caseData.Price);
            ScrollComplete = onScrollComplete;
        }

        private void OnScrollComplete(Common.Scripts.Weapon _)
        {
            ScrollComplete?.Invoke();
        }

        private void OpenAgain()
        {
            if (_walletRoot.TryTakeMoney(_lastCaseData.Price) == false)
                return;
            
            Open(_lastCaseData, true);
        }
    }
}
