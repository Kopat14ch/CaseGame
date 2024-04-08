using Sources.Modules.Case.Scripts;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerRoot : MonoBehaviour
    {
        [SerializeField] private CaseData _caseData;

        private CaseOpenerHandler _caseOpenerHandler;

        [Inject]
        public void Construct(CaseOpenerHandler caseOpenerHandler)
        {
            _caseOpenerHandler = caseOpenerHandler;
            Debug.Log("Init2");
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
