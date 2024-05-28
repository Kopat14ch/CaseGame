using Lean.Localization;

namespace Sources.Modules.Case.Scripts
{
    public class RewardCaseView : CaseView
    {
        protected override void UpdateText()
        {
            OpenText.text = $"{LeanLocalization.GetTranslationText("Open")}";
        }
    }
}