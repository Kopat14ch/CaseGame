using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;
using LeanLocalization = Lean.Localization.LeanLocalization;

namespace Sources.Modules.Localization.Scripts
{
    public class LocalizationHandler
    {
        public LocalizationHandler(LeanLocalization leanLocalization)
        {
            LeanLocalization.Instances.Add(leanLocalization);
            LeanLocalization.UpdateTranslations();

#if UNITY_EDITOR
            SetCurrentLanguage(leanLocalization.CurrentLanguage);
            return;
#endif
            
            switch (YandexGamesSdk.Environment.i18n.lang)
            {
                case "ru":
                    SetCurrentLanguage("Russian");
                    break;
                case "en":
                    SetCurrentLanguage("English");
                    break;
                case "tr":
                    SetCurrentLanguage("Turkish");
                    break;
                default:
                    SetCurrentLanguage("Russian");
                    break;
            }
        }

        private void SetCurrentLanguage(string language)
        {
            LeanLocalization.SetCurrentLanguageAll(language);
        }
    }
}