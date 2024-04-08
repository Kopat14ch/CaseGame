using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Weapon.Scripts
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private WeaponData.WeaponData _weaponData;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _fullNameWeaponText;
        [SerializeField] private Image _backgroundTextImage;

        public void Awake()
        {
            Init();
        }

        public void UpdateData(WeaponData.WeaponData weaponData)
        {
            _weaponData = weaponData;
            
            Init();
        }

        private void Init()
        {
            _image.sprite = _weaponData.Sprite;
            _fullNameWeaponText.text = $"{_weaponData.Name} \n{_weaponData.SkinName}";
        }
    }
}