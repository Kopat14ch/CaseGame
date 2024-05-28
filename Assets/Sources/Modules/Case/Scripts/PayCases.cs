using System;
using System.Collections.Generic;
using Sources.Modules.Case.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Case.Scripts
{
    public class PayCases : MonoBehaviour
    {
        [SerializeField] private List<PayCaseRoot> _payCaseRoots;

        public List<IPayCaseRoot> PayCaseRoots { get; private set; }

        [Inject]
        public void Construct()
        {
            PayCaseRoots = new List<IPayCaseRoot>();
            PayCaseRoots.AddRange(_payCaseRoots);
        }
    }
}