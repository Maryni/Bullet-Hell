using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "Weapon/WeaponStats")]
    public class WeaponStats : ScriptableObject
    {
        public WeaponType weaponType;
        public int bulletCount; //не испозуется
        public float cooldownTime; //не испозуется
        public float shooringRate;
    }
}