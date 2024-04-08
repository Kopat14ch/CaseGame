using System;
using Sources.Modules.Weapon.Scripts;
using UnityEngine;

namespace Sources.Modules.Common.Scripts
{
    [RequireComponent(typeof(WeaponView))]
    public abstract class Weapon : MonoBehaviour
    {
        protected WeaponView View { get; private set; }
        
        private void Awake()
        {
            View = GetComponent<WeaponView>();
        }
    }
}