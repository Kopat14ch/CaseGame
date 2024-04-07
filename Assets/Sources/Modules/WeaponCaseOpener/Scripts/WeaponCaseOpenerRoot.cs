using UnityEngine;

namespace Sources.Modules.WeaponCaseOpener.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponCaseOpenerRoot : Common.Scripts.Weapon
    {
        private Rigidbody2D _rigidbody2D;
        
        public override void Awake()
        {
            base.Awake();

            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void UpdateSpeed(float speed)
        {
            _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        }

        public void OnDisableEnter(Transform parent)
        {
            transform.SetParent(null);
            transform.SetParent(parent);
        }
    }
}