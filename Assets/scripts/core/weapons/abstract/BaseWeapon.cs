using System.Collections;
using System;
using UnityEngine;
using Global.Managers.Datas;
using Global.Controllers;
using Global.Managers;

namespace Global.Shooting
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private WeaponType weaponType;
        [SerializeField] protected WeaponData weaponStats;
#pragma warning restore

        #endregion Inspector variables

        #region properties

        public WeaponType WeaponType => weaponType;
        public WeaponData WeaponStats => weaponStats;
        public int BulletCountCurrent => bulletCountCurrent;

        #endregion properties

        #region protected variables

        protected int bulletCountCurrent;
        protected float correctAngleForSprite = 90f;

        #endregion protected variables

        #region private variables

        private Coroutine coroutineShot;
        private HUDController hUDController;

        #endregion private variables

        #region public void

        public void SetHUDController(HUDController hUDController)
        {
            this.hUDController = hUDController;
        }

        public virtual void Init()
        {
            weaponStats = Services.GetManager<DataManager>().DynamicData.GetWeaponDataByType(weaponType);
        }

        public void Shoot(Vector2 mousePos, Transform transformParent, bool enableRotation)
        {
            if (coroutineShot == null)
            {
                coroutineShot = StartCoroutine(Shooting(mousePos, transformParent, enableRotation));
            }
        }

        public void SetWeaponType(WeaponType weaponType)
        {
            this.weaponType = weaponType;
        }

        public void ResetCoroutineShot()
        {
            coroutineShot = null;
        }

        #endregion public void

        #region protected void

        protected virtual IEnumerator Shooting(Vector2 mousePos, Transform transformParent, bool enableRotation)
        {
            bulletCountCurrent--;
            float zParentRotation = gameObject.transform.parent.transform.rotation.eulerAngles.z;
            var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
            bullet.transform.position = transformParent.position;
            bullet.gameObject.SetActive(true);
            bullet.ActivateBullet();
            bullet.Rotate(zParentRotation);
            bullet.Move();
            if (enableRotation)
            {
                bullet.Rotate(correctAngleForSprite + zParentRotation);
            }
            yield return new WaitForSeconds(weaponStats.shootingRate);
            if (bulletCountCurrent == 0)
            {
                yield return Reload();
            }
            StopCoroutineShooting();
        }

        protected void StopCoroutineShooting()
        {
            StopCoroutine(coroutineShot);
            coroutineShot = null;
        }

        protected IEnumerator Reload()
        {
            yield return new WaitForSeconds(weaponStats.cooldownTime);
            bulletCountCurrent = weaponStats.bulletCount;
            hUDController.GlowingByType(TypeGlowing.BulletsCurrent, bulletCountCurrent);
        }

        #endregion protected void
    }
}