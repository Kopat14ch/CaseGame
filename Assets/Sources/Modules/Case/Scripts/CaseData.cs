using System;
using System.Collections.Generic;
using Sources.Modules.Weapon.Scripts;
using UnityEngine;

namespace Sources.Modules.Case.Scripts
{
    [CreateAssetMenu(fileName = "NewCase", menuName = "Cases/CaseData", order = 0)]
    public class CaseData : ScriptableObject
    {
        [field: SerializeField] public String Name { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public List<WeaponData> Weapons { get; private set; }
    }
}