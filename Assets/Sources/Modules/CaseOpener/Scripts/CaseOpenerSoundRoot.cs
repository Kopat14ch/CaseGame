using Sources.Modules.CaseOpener.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerSoundRoot : MonoBehaviour
    {
        [SerializeField] private AudioSource _endScrollSource;
        
        private ICaseOpenerHandler _caseOpenerHandler;

        [Inject]
        public void Construct(ICaseOpenerHandler caseOpenerHandler)
        {
            _caseOpenerHandler = caseOpenerHandler;
        }
        
        private void OnEnable()
        {
            _caseOpenerHandler.ScrollComplete += OnScrollComplete;
        }

        private void OnDisable()
        {
            _caseOpenerHandler.ScrollComplete -= OnScrollComplete;
        }
        
        private void OnScrollComplete(Common.Scripts.Weapon weapon)
        {
            _endScrollSource.Play();
        }
    }
}