using Global.Bullet;
using Global.Managers;
using Global.Shooting;
using System;
using System.Collections;
using UnityEngine;

namespace Global.Weapon
{
    public class RocketLaucherWeapon : BaseWeapon
    {
        #region private variables

        private int bulletCountCurrent;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            Init();
            bulletCountCurrent = weaponStats.bulletCount;
        }

        #endregion Unity functions

        #region public void

        public override IEnumerator Shoot(Vector2 mousePos, Transform transformParent, Action callback = null)
        {
            if (bulletCountCurrent == 0)
            {
                yield return Reload();
            }

            bulletCountCurrent--;
            float zParentRotation = gameObject.transform.parent.transform.rotation.eulerAngles.z;
            var bullet = (RocketLaucherBullet)Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
            bullet.transform.position = transformParent.position;
            bullet.gameObject.SetActive(true);
            bullet.Rotate(zParentRotation);
            bullet.Move();
            yield return new WaitForSeconds(weaponStats.shooringRate);
            callback?.Invoke();
        }

        protected override IEnumerator Reload()
        {
            yield return new WaitForSeconds(weaponStats.cooldownTime);
            bulletCountCurrent = weaponStats.bulletCount;
        }

        #endregion public void
    }
}