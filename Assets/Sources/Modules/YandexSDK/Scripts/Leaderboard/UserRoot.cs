using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Modules.YandexSDK.Scripts.Leaderboard
{
    public class UserRoot : MonoBehaviour
    {
        [SerializeField] private TMP_Text _topText;
        [SerializeField] private TMP_Text _nickNameText;
        [SerializeField] private TMP_Text _levelText;
        
        public int Level { get; private set; }

        public void Init(int top, string nickName, int level, Color? color = null)
        {
            Level = level;
            
            if (color != null)
                _topText.color = (Color)color;
            
            _topText.text = top.ToString();
            _nickNameText.text = nickName;
            _levelText.text = level.ToString();
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }
    }
}