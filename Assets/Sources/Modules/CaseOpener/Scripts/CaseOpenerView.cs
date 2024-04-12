using Sources.Modules.CaseOpener.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CaseOpenerView : MonoBehaviour
    {
        [SerializeField] private Button _takeButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private TMP_Text _takeButtonText;
        [SerializeField] private TMP_Text _sellButtonText;
        [SerializeField] private TMP_Text _winItemText;

        private CanvasGroup _canvasGroup;
        private ICaseOpenerHandler _caseOpenerHandler;
        private CaseOpenerContent _content;

        public CaseOpenerContent Content => _content;

        [Inject]
        public void Construct(ICaseOpenerHandler caseOpenerHandler, CaseOpenerContent contentCaseOpener)
        {
            _content = contentCaseOpener;
            _caseOpenerHandler = caseOpenerHandler;
            _canvasGroup = GetComponent<CanvasGroup>();
            DisableWinUI();
        }
        
        private void OnEnable()
        {
            _takeButton.onClick.AddListener(OnTakeButtonClick);
            _sellButton.onClick.AddListener(OnSellButtonClick);
            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
        }

        private void OnDisable()
        {
            _takeButton.onClick.RemoveListener(OnTakeButtonClick);
            _sellButton.onClick.RemoveListener(OnSellButtonClick);
            
            _caseOpenerHandler.ScrollComplete -= OnScrollComplete;
        }

        public void EnableView()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1;
            _content.DisableLayout();
        }

        public void DisableView()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0;
            _content.transform.localPosition = Vector3.zero;
            _content.EnableLayout();
            _content.EnableWeapons();
            DisableWinUI();
        }


        private void OnTakeButtonClick()
        {
            DisableView();
        }

        private void OnSellButtonClick()
        {
            DisableView();
        }

        private void OnScrollComplete(Common.Scripts.Weapon weapon)
        {
            EnableWinUI();

            _sellButtonText.text = $"Продать \n{weapon.Data.GetCurrentPrice()}$";
            _winItemText.text = $"Вы выбили: {weapon.Data.Name}\n{weapon.Data.SkinName}";
        }

        private void EnableWinUI()
        {
            _sellButton.gameObject.SetActive(true);
            _takeButton.gameObject.SetActive(true);
            _winItemText.gameObject.SetActive(true);
        }

        private void DisableWinUI()
        {
            _sellButton.gameObject.SetActive(false);
            _takeButton.gameObject.SetActive(false);
            _winItemText.gameObject.SetActive(false);
        }
    }
}