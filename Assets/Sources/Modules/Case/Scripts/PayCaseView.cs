using Lean.Localization;
using Sources.Modules.Case.Interfaces;

namespace Sources.Modules.Case.Scripts
{
    public class PayCaseView : CaseView
    {
        private const int Price = 10;

        private IPayCaseRoot _payCaseRoot;

        public void Init(IPayCaseRoot payCaseRoot)
        {
            if (_payCaseRoot == null)
            {
                _payCaseRoot = payCaseRoot;
                _payCaseRoot.PurchaseUpdated += UpdateText;
                UpdateText();
            }
        }
        
        private void OnDestroy()
        {
            _payCaseRoot.PurchaseUpdated -= UpdateText;
        }

        protected override void UpdateText()
        {
            if (_payCaseRoot.IsPurchase)
                OpenText.text = OpenText.text = $"{LeanLocalization.GetTranslationText("Open")}";
            else
                OpenText.text = OpenText.text = $"{LeanLocalization.GetTranslationText("Open")}: {Price}";
        }
        
    }
}