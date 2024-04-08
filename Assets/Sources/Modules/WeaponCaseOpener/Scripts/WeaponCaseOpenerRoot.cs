using Sources.Modules.Common.Interfaces;
using Sources.Modules.Weapon.WeaponData;
using UnityEngine;
using Zenject;

namespace Sources.Modules.WeaponCaseOpener.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponCaseOpenerRoot : Common.Scripts.Weapon
    {
        private Rigidbody2D _rigidbody2D;
        private ISpeedUpdater _speedUpdater;


        [Inject]
        public void Construct(ISpeedUpdater speedUpdater)
        {
            _speedUpdater = speedUpdater;
            
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable()
        {
            _speedUpdater.SpeedUpdated += UpdateSpeed;
        }

        private void OnDisable()
        {
            _speedUpdater.SpeedUpdated -= UpdateSpeed;
        }

        public void Init(WeaponData weaponData)
        {
            View.UpdateData(weaponData);
        }
        
        public void Disable()
        {
            
        }

        private void UpdateSpeed(float speed)
        {
            _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        }
    }
}