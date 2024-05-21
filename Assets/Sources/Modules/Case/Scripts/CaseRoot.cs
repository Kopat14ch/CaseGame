using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Wallet.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Sources.Modules.Case.Scripts
{
    public class CaseRoot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CaseData _data;
        [SerializeField] private Transform _imageTransform;
        
        private CaseView _caseView;
        private IWalletRoot _walletRoot;
        private CaseHandler _caseHandler;
        
        protected ICaseOpener CaseOpenerRoot { get; private set; }
        protected CaseData Data => _data;

        [Inject]
        public void Construct(ICaseOpener caseOpenerRoot, IWalletRoot walletRoot)
        {
            _walletRoot = walletRoot;
            CaseOpenerRoot = caseOpenerRoot;
            _caseView = GetComponent<CaseView>();

            if (_caseView != null)
                _caseView.Init(_data);

            _caseHandler = new CaseHandler(_imageTransform);
        }
        
        private void OnEnable()
        {
            _caseView.OpenButtonClicked += OnOpenButtonClicked;
        }

        private void OnDisable()
        {
            _caseView.OpenButtonClicked -= OnOpenButtonClicked;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _caseHandler.MouseEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _caseHandler.MouseExit();
        }

        protected virtual void OnOpenButtonClicked()
        {
            if (_walletRoot.TryTakeMoney(_data.Price) == false)
                return;
            
            CaseOpenerRoot.Open(_data);
        }
    }
}
