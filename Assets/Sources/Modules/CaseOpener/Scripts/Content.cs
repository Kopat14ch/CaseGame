using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.CaseOpener.Scripts
{
    public class Content : MonoBehaviour
    {
        private HorizontalLayoutGroup _horizontalLayoutGroup;
        
        private void Awake()
        {
            _horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        }

        public void EnableLayout()
        {
            _horizontalLayoutGroup.enabled = true;
        }

        public void DisableLayout()
        {
            _horizontalLayoutGroup.enabled = false;
        }
    }
}