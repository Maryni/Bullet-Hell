using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Global.Managers.Datas
{
    public enum WeaponType
    {
        AutomaticGun,
        Shotgun,
        RocketLaucher,
        MeleeAttack
    }

    public enum EnemyType
    {
        MeleeGrounded,
        MeleeFlyed,
        ShootingGrounded,
        ShootingFlyed
    }

    [CreateAssetMenu(fileName = "StaticData", menuName = "Data/StaticData")]
    public class StaticData : ScriptableObject
    {
        [SerializeField] private WeaponStats[] weaponStats;

        public WeaponStats GetWeaponStatsByType(WeaponType weaponType)
        {
            return weaponStats.FirstOrDefault(x => x.weaponType == weaponType);
        }
    }
}