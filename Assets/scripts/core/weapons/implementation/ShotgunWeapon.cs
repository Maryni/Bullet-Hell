using Global.Bullet;
using Global.Managers;
using Global.Shooting;
using System;
using System.Collections;
using UnityEngine;

namespace Global.Weapon
{
    public class ShotgunWeapon : BaseWeapon
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private int countBulletForShot = 4;
        [SerializeField] private float maxAngle = 70;
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private float angleBullet;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            Init();
            AddOrRemoveValueToInt();
            bulletCountCurrent = weaponStats.bulletCount;
        }

        #endregion Unity functions

        #region public void

        protected override IEnumerator Shoot(Vector2 mousePos, Transform transformParent, Action callback = null)
        {
            if (bulletCountCurrent <= 0)
            {
                yield return Reload();
            }

            bulletCountCurrent -= countBulletForShot;
            float angleStep = maxAngle / (countBulletForShot - 1);
            float zParentRotation = gameObject.transform.parent.transform.rotation.eulerAngles.z;
            var i = 0;
            while (i < countBulletForShot)
            {
                if (i == 0)
                {
                    angleBullet = -(maxAngle / 2);
                }
                else
                {
                    if (countBulletForShot != 1)
                    {
                        angleBullet += angleStep;
                    }
                }
                if (countBulletForShot == 1)
                {
                    angleBullet = 0;
                }

                var bullet = (ShotgunBullet)Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
                bullet.transform.position = transformParent.position;
                bullet.gameObject.SetActive(true);
                bullet.Rotate(angleBullet + zParentRotation);
                bullet.Move();

                i++;
                yield return null;
            }
            yield return new WaitForSeconds(weaponStats.shootingRate);
            StartCoroutine(base.Shoot(mousePos, transformParent, callback));
            callback?.Invoke();
        }

        #endregion public void

        #region private void

        private void AddOrRemoveValueToInt()
        {
            if (weaponStats.bulletCount % countBulletForShot != 0)
            {
                if (weaponStats.bulletCount / countBulletForShot > countBulletForShot / 2) // if bulletCount (11) / countBulletForShot (4)  > countBulletForShot/2 (2), 11 / 4  (== 2.75) > 2
                {
                    weaponStats.bulletCount++;
                }
                else
                {
                    weaponStats.bulletCount--;
                }
                Start();
            }
        }

        #endregion private void
    }
}