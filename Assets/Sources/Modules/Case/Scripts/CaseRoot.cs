using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Wallet.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Sources.Modules.Case.Scripts
{
    [RequireComponent(typeof(CaseView))]
    public class CaseRoot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CaseData _data;
        [SerializeField] private Transform _imageTransform;
        
        private CaseView _caseView;
        private ICaseOpener _caseOpenerRoot;
        private IWalletRoot _walletRoot;
        private CaseHandler _caseHandler;

        [Inject]
        public void Construct(ICaseOpener caseOpenerRoot, IWalletRoot walletRoot)
        {
            _walletRoot = walletRoot;
            _caseOpenerRoot = caseOpenerRoot;
            _caseView = GetComponent<CaseView>();
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

        private void OnOpenButtonClicked()
        {
            if (_walletRoot.TryTakeMoney(_data.Price) == false)
                return;
            
            _caseOpenerRoot.Open(_data);
        }
    }
}
