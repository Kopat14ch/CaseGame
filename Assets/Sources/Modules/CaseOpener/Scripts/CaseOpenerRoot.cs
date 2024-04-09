using System;
using Sources.Modules.Case.Scripts;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CaseOpenerRoot : MonoBehaviour, ICaseOpener
    {
        [SerializeField] private CaseData _caseData;
        [SerializeField] private CaseOpenerArrow _caseOpenerArrow;
        [SerializeField] private Content _content;
        
        private CanvasGroup _canvasGroup;
        private CaseOpenerHandler _caseOpenerHandler;

        [Inject]
        public void Construct(CaseOpenerHandler caseOpenerHandler)
        {
            _caseOpenerHandler = caseOpenerHandler;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Disable();
        }
        
        public void Open(WeaponData[] weaponDatas)
        {
            Enable();
            _caseOpenerHandler.Open(weaponDatas,_caseOpenerArrow,_content.transform);
        }
        
        public void Open()
        {
            _caseOpenerHandler.Open(_caseData.Weapons,_caseOpenerArrow, _content.transform);
            _content.DisableLayout();
        }

        private void Enable()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1;
            _content.DisableLayout();
        }

        private void Disable()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0;
            _content.EnableLayout();
        }
    }
}
