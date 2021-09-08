using Global.Bullet;
using Global.Managers;
using Global.Managers.Datas;
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
            SetValueFromData();
            bulletCountCurrent = weaponStats.bulletCount;
        }

        #endregion Unity functions

        #region public void

        protected override IEnumerator Shooting(Vector2 mousePos, Transform transformParent, bool enableRotation)
        {
            bulletCountCurrent--;
            BaseBullet[] bullets = new BaseBullet[countBulletForShot];
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

                var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
                bullet.transform.position = transformParent.position;
                bullet.gameObject.SetActive(true);
                bullet.ActivateBullet();
                bullet.Rotate(angleBullet + zParentRotation);
                bullets[i] = bullet;

                i++;
                yield return null;
            }
            for (int j = 0; j < bullets.Length; j++)
            {
                bullets[j].Move();
            }

            yield return new WaitForSeconds(weaponStats.shootingRate);
            if (bulletCountCurrent <= 0)
            {
                yield return Reload();
            }
            base.StopCoroutineShooting();
        }

        #endregion public void

        #region private void

        private void SetValueFromData()
        {
            var data = Services.GetManager<DataManager>().DynamicData;
            countBulletForShot = data.ShotgunData.countBulletsInOnceShoot;
            maxAngle = data.ShotgunData.angleBullets;
        }

        #endregion private void
    }
}