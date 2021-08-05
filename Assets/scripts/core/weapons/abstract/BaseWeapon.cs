using System.Collections;
using System.Collections.Generic;
using Global.Interfaces.Weapon;
using Global.Settings;
using Global.Shooting.BulletSpace;
using System;
using UnityEngine;
using Global.Managers.Datas;

namespace Global.Shooting
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;

        [SerializeField] protected WeaponStats weaponStats;

        public WeaponType WeaponType => weaponType;

        protected WeaponStats Stats => weaponStats;

        public virtual void Init()
        {
            weaponStats = Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(weaponType);
        }

        public virtual void Reload()
        {
        }

        public void Shoot()
        {
            StartCoroutine();
        }

        public abstract IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent, Action callback = null);
    }
}