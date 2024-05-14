using Lean.Localization;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Localization.Scripts
{
    public class LocalizationRoot : MonoBehaviour
    {
        [Inject]
        public void Construct()
        {
            LocalizationHandler handler = new LocalizationHandler(GetComponent<LeanLocalization>());
        }
    }
}
