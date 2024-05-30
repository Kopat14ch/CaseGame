using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.Web
{
    public class WebEventSystem : EventSystem
    {
        protected override void OnApplicationFocus(bool hasFocus) => base.OnApplicationFocus(true);
    }
}
