using System;
using System.Collections.Generic;
using Sources.Modules.Weapon.Scripts;
using Sources.Modules.Weapon.WeaponData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Case.Scripts
{
    public class CaseView : MonoBehaviour
    {
        [SerializeField] private CaseData _caseData;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Button _openButton;

        public event Action<WeaponData[]> OpenButtonClicked;

        public void Awake()
        {
            _image.sprite = _caseData.Sprite;
            _nameText.text = _caseData.Name;
        }

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnOpenButtonClick);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClick);
        }

        private void OnOpenButtonClick() => OpenButtonClicked?.Invoke(_caseData.Weapons);
    }
}
