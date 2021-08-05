using Global.Bullet;
using Global.Managers;
using Global.Managers.Datas;
using Global.Shooting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Weapon
{
    public class ShotgunWeapon : BaseWeapon
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private int countBulletForShot = 5;
        [SerializeField] private float angleBullet;
        [SerializeField] private float defaultAngleBullet = 15;
        [SerializeField] private int countSplitLines = 2;//2 - for cut countBulletForShot on 2 parts, left part from main bullet without rotation, and right part, not change it
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            Init();
        }

        #endregion Unity functions

        #region public void

        public override IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent, Action callback = null)
        {
            var i = 0;
            while (i < countBulletForShot)
            {
                var bullet = (ShotgunBullet)Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
                angleBullet = (-defaultAngleBullet * Mathf.FloorToInt(countBulletForShot / countSplitLines)) + (defaultAngleBullet * i);
                bullet.Rotate(angleBullet);
                bullet.transform.position = transformParent.position;
                bullet.gameObject.SetActive(true);
                bullet.Move(transformCanon);
                i++;
                yield return null;
            }
            yield return new WaitForSeconds(weaponStats.shooringRate);
            callback?.Invoke();
        }

        #endregion public void
    }
}