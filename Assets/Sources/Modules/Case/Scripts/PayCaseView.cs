using Lean.Localization;

namespace Sources.Modules.Case.Scripts
{
    public class PayCaseView : CaseView
    {
        private const int Price = 10;
        
        public override void UpdateText()
        {
            OpenText.text = OpenText.text = $"{LeanLocalization.GetTranslationText("Open")}: {Price}";
        }
    }
}