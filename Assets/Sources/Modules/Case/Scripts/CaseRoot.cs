using Sources.Modules.CaseOpener.Scripts;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Case.Scripts
{
    [RequireComponent(typeof(CaseView))]
    public class CaseRoot : MonoBehaviour
    {
        private CaseView _caseView;
        private CaseOpenerRoot _caseOpenerRoot;


        [Inject]
        public void Construct(CaseOpenerRoot caseOpenerRoot)
        {
            _caseOpenerRoot = caseOpenerRoot;
            _caseView = GetComponent<CaseView>();
        }
        
        private void OnEnable()
        {
            _caseView.OpenButtonClicked += OnOpenButtonClicked;
        }

        private void OnDisable()
        {
            _caseView.OpenButtonClicked -= OnOpenButtonClicked;
        }

        private void OnOpenButtonClicked(WeaponData[] weaponDatas)
        {
            _caseOpenerRoot.Open(weaponDatas);
        }
    }
}
