using System;
using Global.Bullet;
using Global.Managers;
using Global.Managers.Datas;
using Global.Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Weapon
{
    public class ShotgunWeapon : BaseWeapon
    {
        private int countBulletForShot = 5;

        private void Start()
        {
            Init();
        }

        public override IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent, Action callback = null)
        {
            int i = 0;
            while (i < countBulletForShot)
            {
                var bullet = (ShotgunBullet)Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
                //bullet.Rotate();
                bullet.BulletStatsShotgun.angel = (-bullet.BulletStatsShotgun.defaultAngel * 2) +
                                                  (bullet.BulletStatsShotgun.defaultAngel * countBulletForShot);

                bullet.transform.position = transformParent.position;
                bullet.gameObject.SetActive(true);
                bullet.Move(mousePos, transformCanon);
                i++;
                yield return null;
            }

            yield return new WaitForSeconds(weaponStats.shooringRate);
            callback?.Invoke();
        }
    }
}