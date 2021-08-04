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
        private int countBulletForShot = 0;

        private void Start()
        {
            Init();
        }

        public override IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent)
        {
            if (countBulletForShot == 4)
            {
                countBulletForShot = 0;
            }
            var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
            bullet.BulletStatsShotgun.angel = (-bullet.BulletStatsShotgun.defaultAngel * 2) + (bullet.BulletStatsShotgun.defaultAngel * countBulletForShot);
            countBulletForShot++;

            bullet.transform.position = transformParent.position;
            bullet.gameObject.SetActive(true);
            bullet.Move(mousePos, transformCanon);
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}