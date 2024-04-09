using Sources.Modules.Common.Interfaces;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.WeaponCaseOpener.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(LayoutElement))]
    public class WeaponCaseOpenerRoot : Common.Scripts.Weapon
    {
        private Rigidbody2D _rigidbody2D;
        private ISpeedUpdater _speedUpdater;

        [Inject]
        public void Construct(ISpeedUpdater speedUpdater)
        {
            _speedUpdater = speedUpdater;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            _speedUpdater.SpeedUpdated += UpdateSpeed;
        }

        private void OnDestroy()
        {
            _speedUpdater.SpeedUpdated -= UpdateSpeed;
        }

        public void Init(WeaponData weaponData)
        {
            View.UpdateData(weaponData);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void UpdateSpeed(float speed)
        {
            _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        }
    }
}