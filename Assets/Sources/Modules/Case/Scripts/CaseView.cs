using System;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Case.Scripts
{
    public class CaseView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _openButton;
        [SerializeField] private TMP_Text _nameText;
        [field: SerializeField] protected TMP_Text OpenText { get; private set; }

        public event Action OpenButtonClicked;

        protected CaseData Data { get; private set; }
        
        private void Awake()
        {
            _image.sprite = Data.Sprite;
            _nameText.text = Data.Name;
        }

        public void Init(CaseData caseData)
        {
            Data = caseData;
            UpdateText();
        }

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnOpenButtonClick);
        }

        protected virtual void UpdateText()
        {
            OpenText.text = $"{LeanLocalization.GetTranslationText("Open")} \n{Data.Price}$";
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClick);
        }

        private void OnOpenButtonClick()
        {
            OpenButtonClicked?.Invoke();
        }
    }
}
