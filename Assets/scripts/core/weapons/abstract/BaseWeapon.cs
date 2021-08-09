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
        #region Inspector variables

#pragma warning disable
        [SerializeField] private WeaponType weaponType;
        [SerializeField] protected WeaponStats weaponStats;
#pragma warning restore

        #endregion Inspector variables

        #region properties

        public WeaponType WeaponType => weaponType;
        public WeaponStats WeaponStats => weaponStats;

        #endregion properties

        #region public void

        public virtual void Init()
        {
            weaponStats = Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(weaponType);
        }

        public abstract IEnumerator Reload();

        public virtual IEnumerator Shoot(Vector2 mousePos, Transform transformParent, Action callback = null)
        {
            StartCoroutine(Shoot(mousePos, transformParent, callback: null));
            yield return new WaitForSeconds(weaponStats.shooringRate);
        }

        #endregion public void
    }
}