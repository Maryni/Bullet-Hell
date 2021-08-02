using Global.Interfaces.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Interfaces.Weapon
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Weapon/Settings", order = 4)]
    public class WeaponSettings : ScriptableObject, IWeaponStats
    {
        #region private variables

#pragma warning disable
        [SerializeField] private string weaponName;
        [SerializeField] private int bulletCount;
        [SerializeField] private float cooldownTime;
        [SerializeField] private float damage;
        [SerializeField] private float shooringRate;
        [SerializeField] private int bulletOnShotUsed;

        [SerializeField] public enum typeBullet { AutomaticGun, Shotgun, RocketLaucher };

#pragma warning restore

        #endregion private variables

        #region properties

        public string WeaponName => weaponName;
        public int BulletsCount => bulletCount;
        public float CooldownTime => cooldownTime;
        public float Damage => damage;
        public float ShootingRate => shooringRate;
        public int BulletOnShotUsed => bulletOnShotUsed;
        public typeBullet TypeBulletRocket => typeBullet.RocketLaucher;
        public typeBullet TypeBulletAutomatic => typeBullet.AutomaticGun;
        public typeBullet TypeBulletShotgun => typeBullet.Shotgun;

        #endregion properties
    }
}