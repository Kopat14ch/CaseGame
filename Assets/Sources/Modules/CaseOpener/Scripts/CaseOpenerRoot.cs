using System;
using Sources.Modules.Case.Scripts;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerRoot : MonoBehaviour
    {
        [SerializeField] private CaseData _caseData;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;

        private CaseOpenerHandler _caseOpenerHandler;

        [Inject]
        public void Construct(CaseOpenerHandler caseOpenerHandler)
        {
            _caseOpenerHandler = caseOpenerHandler;
        }

        private void Awake()
        {
            _horizontalLayoutGroup.enabled = false;
        }

        public void Open(WeaponData[] weaponDatas)
        {
            _caseOpenerHandler.Open(weaponDatas);
        }
        
        public void Open()
        {
            _caseOpenerHandler.Open(_caseData.Weapons);
        }
    }
}
