using System.Linq;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts.Leaderboard
{
    public class UsersContainer : MonoBehaviour
    {
        public void Clear()
        {
            GetComponentsInChildren<UserRoot>().ToList().ForEach(root => Destroy(root.gameObject));
        }
    }
}