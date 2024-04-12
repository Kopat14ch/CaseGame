using Sources.Modules.Case.Scripts;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    [RequireComponent(typeof(CaseOpenerView))]
    public class CaseOpenerRoot : MonoBehaviour, ICaseOpener
    {
        [SerializeField] private CaseData _caseData;
        [SerializeField] private CaseOpenerArrow _caseOpenerArrow;
        
        private CaseOpenerHandler _caseOpenerHandler;
        private CaseOpenerView _caseOpenerView;
        private CaseOpenerContent _contentCaseOpener;

        [Inject]
        public void Construct(CaseOpenerHandler caseOpenerHandler)
        {
            _caseOpenerHandler = caseOpenerHandler;
            _caseOpenerView = GetComponent<CaseOpenerView>();
        }

        private void Start()
        {
            _caseOpenerView.DisableView();
        }
        
        public void Open(WeaponData[] weaponDatas)
        {
            _caseOpenerHandler.Open(weaponDatas,_caseOpenerArrow, _caseOpenerView.Content.transform);
            _caseOpenerView.EnableView();
        }
    }
}
