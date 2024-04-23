using System;
using Sources.Modules.CaseOpener.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class CaseOpenerWeaponSoundRoot : MonoBehaviour
    {
       [SerializeField] private AudioSource _enterSource;
       
       private ICaseOpenerView _caseOpenerView;

       [Inject]
       public void Construct(ICaseOpenerView caseOpenerView)
       {
           _caseOpenerView = caseOpenerView;
       }

       private void OnTriggerEnter2D(Collider2D other)
       {
           if (other.TryGetComponent(out CaseOpenerArrow _) && _caseOpenerView.IsEnable())
               _enterSource.Play();
       }

    }
}