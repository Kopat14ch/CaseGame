using System.Collections;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexSDKInitialize : MonoBehaviour
    {
#if UNITY_EDITOR == false
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize(YandexGamesSdk.GameReady);
        }
#endif
    }
}