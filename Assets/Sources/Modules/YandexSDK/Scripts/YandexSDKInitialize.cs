using Agava.YandexGames;
using Cysharp.Threading.Tasks;
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

        private async void Start()
        {
            await YandexGamesSdk.Initialize();

        }
#endif
    }
}