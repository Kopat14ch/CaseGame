using System;
using Sources.Modules.CaseOpener.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CaseOpenerView : MonoBehaviour, ICaseOpenerView
    {
        [SerializeField] private Button _takeButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _openAgainButton;
        [SerializeField] private TMP_Text _takeButtonText;
        [SerializeField] private TMP_Text _sellButtonText;
        [SerializeField] private TMP_Text _winItemText;
        [SerializeField] private TMP_Text _openAgainButtonText;

        private CanvasGroup _canvasGroup;
        private ICaseOpenerHandler _caseOpenerHandler;
        private CaseOpenerContent _content;
        private Common.Scripts.Weapon _currentWeapon;
        
        public CaseOpenerContent Content => _content;
        
        public event Action SellButtonClicked;
        public event Action OpenAgainButtonClicked;

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
            _openAgainButton.onClick.AddListener(OpenAgain);
            
            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
        }

        private void OnDisable()
        {
            _takeButton.onClick.RemoveListener(OnTakeButtonClick);
            _sellButton.onClick.RemoveListener(OnSellButtonClick);
            _openAgainButton.onClick.RemoveListener(OpenAgain);
            
            _caseOpenerHandler.ScrollComplete -= OnScrollComplete;
        }

        public void EnableView(float casePrice)
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1;
            _openAgainButtonText.text = $"Еще раз\n{casePrice}$";
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
            SellButtonClicked?.Invoke();
        }

        private void OnScrollComplete(Common.Scripts.Weapon weapon)
        {
            EnableWinUI();

            _sellButtonText.text = $"Продать \n{Math.Round(weapon.Price, 2)}$";
            _winItemText.text = $"Вы выбили: {weapon.Data.GetName()}\n{weapon.Data.SkinName}";
        }

        private void EnableWinUI()
        {
            _sellButton.gameObject.SetActive(true);
            _takeButton.gameObject.SetActive(true);
            _winItemText.gameObject.SetActive(true);
            _openAgainButton.gameObject.SetActive(true);
        }

        private void DisableWinUI()
        {
            _sellButton.gameObject.SetActive(false);
            _takeButton.gameObject.SetActive(false);
            _winItemText.gameObject.SetActive(false);
            _openAgainButton.gameObject.SetActive(false);
        }

        private void OpenAgain()
        {
            OpenAgainButtonClicked?.Invoke();
        }
    }
}