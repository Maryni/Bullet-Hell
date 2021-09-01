using System.Collections;
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
        public int BulletCountCurrent => bulletCountCurrent;

        #endregion properties

        #region protected variables

        protected int bulletCountCurrent;

        #endregion protected variables

        #region private variables

        private Coroutine coroutineShot;
#pragma warning disable
        private Action actionCallback;
#pragma warning restore

        #endregion private variables

        #region public void

        public virtual void Init()
        {
            weaponStats = Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(weaponType);
        }

        public void Shoot(Vector2 mousePos, Transform transformParent)
        {
            if (coroutineShot == null)
            {
                coroutineShot = StartCoroutine(Shoot(mousePos, transformParent, () => actionCallback = null));
            }
        }

        public void SetWeaponType(WeaponType weaponType)
        {
            this.weaponType = weaponType;
        }

        public void ReserCoroutine()
        {
            coroutineShot = null;
        }

        #endregion public void

        #region protected void

        protected virtual IEnumerator Shoot(Vector2 mousePos, Transform transformParent, Action callback = null)
        {
            yield return null;
            StopCoroutine(Shoot(mousePos, transformParent, () => actionCallback = null));
            coroutineShot = null;
        }

        protected IEnumerator Reload()
        {
            yield return new WaitForSeconds(weaponStats.cooldownTime);
            bulletCountCurrent = weaponStats.bulletCount;
        }

        #endregion protected void
    }
}