using UnityEngine;

namespace Sources.Modules.Utils
{
    public static class CanvasGroupUtil
    {
        public static void Enable(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
        public static void Disable(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}