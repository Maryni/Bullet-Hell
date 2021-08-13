using Global.Bullet;
using Global.Managers;
using Global.Shooting;
using System;
using System.Collections;
using UnityEngine;

namespace Global.Weapon
{
    public class AutomaticGunWeapon : BaseWeapon
    {
        #region Unity functions

        private void Start()
        {
            Init();
            bulletCountCurrent = weaponStats.bulletCount;
        }

        #endregion Unity functions

        #region public void

        protected override IEnumerator Shoot(Vector2 mousePos, Transform transformParent, Action callback = null)
        {
            if (bulletCountCurrent == 0)
            {
                yield return Reload();
            }

            bulletCountCurrent--;
            float zParentRotation = gameObject.transform.parent.transform.rotation.eulerAngles.z;
            var bullet = (AutomaticalBullet)Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
            bullet.transform.position = transformParent.position;
            bullet.gameObject.SetActive(true);
            bullet.Rotate(zParentRotation);
            bullet.Move();
            yield return new WaitForSeconds(weaponStats.shootingRate);
            StartCoroutine(base.Shoot(mousePos, transformParent, callback));
            callback?.Invoke();
        }

        #endregion public void
    }
}