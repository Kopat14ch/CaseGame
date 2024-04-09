using System;
using Sources.Modules.Case.Scripts;
using Sources.Modules.CaseOpener.Interfaces;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerRoot : MonoBehaviour, ICaseOpener
    {
        [SerializeField] private CaseData _caseData;

        private CaseOpenerHandler _caseOpenerHandler;

        [Inject]
        public void Construct(CaseOpenerHandler caseOpenerHandler)
        {
            _caseOpenerHandler = caseOpenerHandler;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Open(WeaponData[] weaponDatas)
        {
            gameObject.SetActive(true);
            _caseOpenerHandler.Open(weaponDatas);
        }
        
        public void Open()
        {
            _caseOpenerHandler.Open(_caseData.Weapons);
        }
    }
}
