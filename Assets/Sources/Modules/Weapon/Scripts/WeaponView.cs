using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Weapon.Scripts
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private WeaponData.BaseWeaponData _weaponData;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _fullNameWeaponText;
        [SerializeField] private Image _backgroundTextImage;

        public void Awake()
        {
            Init();
        }

        public void UpdateData(WeaponData.BaseWeaponData weaponData)
        {
            _weaponData = weaponData;
            
            Init();
        }

        private void Init()
        {
            _image.sprite = _weaponData.Sprite;
            _backgroundTextImage.color = _weaponData.GetCurrentColor();
            _fullNameWeaponText.text = $"{_weaponData.GetName()} \n{_weaponData.SkinName}";
        }
    }
}