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
        RocketLaucher
    }

    public enum EnemyType
    {
        MeleeGrounded_LowSpeed,
        MeleeGrounded_MiddleSpeed,
        MeleeGrounded_FastSpeed,
        MeleeFlyed,
        ShootingGrounded,
        ShootingFlyed
    }

    [Serializable]
    public class EnemyStats
    {
        [SerializeField] public EnemyType enemyType;
        [SerializeField] public int hpMaximum;
        [SerializeField] public float speed;
        [SerializeField] public int hpValueCurrent;
        [SerializeField] public float damage;
        [SerializeField] public int intellegence;
        [SerializeField] public float attackRate;
    }

    [CreateAssetMenu(fileName = "StaticData", menuName = "Data/StaticData")]
    public class StaticData : ScriptableObject
    {
        [SerializeField] private WeaponStats[] weaponStats;
        [SerializeField] private EnemyStats[] enemyStats;

        public WeaponStats GetWeaponStatsByType(WeaponType weaponType)
        {
            return weaponStats.FirstOrDefault(x => x.weaponType == weaponType);
        }

        public EnemyStats GetEnemyStatsByType(EnemyType enemyType)
        {
            return enemyStats.FirstOrDefault(x => x.enemyType == enemyType);
        }
    }
}