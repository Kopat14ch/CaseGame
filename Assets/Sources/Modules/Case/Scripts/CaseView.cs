using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Case.Scripts
{
    public class CaseView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _openText;
        [SerializeField] private Button _openButton;

        public event Action OpenButtonClicked;

        private CaseData _caseData;
        
        public void Awake()
        {
            _image.sprite = _caseData.Sprite;
            _nameText.text = _caseData.Name;
        }

        public void Init(CaseData caseData)
        {
            _caseData = caseData;
            _openText.text = $"Открыть \n{_caseData.Price}$";
        }

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnOpenButtonClick);
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
