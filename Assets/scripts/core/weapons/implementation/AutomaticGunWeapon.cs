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
            bulletCountCurrent--;
            float zParentRotation = gameObject.transform.parent.transform.rotation.eulerAngles.z;
            var bullet = /*(AutomaticalBullet)*/Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
                //зачем ты преобразовуешь пулу в конкретную пулю?
                //у тебя реализация бейс булета такая, что ты можешь напрямую вызывать нужные тебе функции, которые ты реализовал в бейс булете
            bullet.transform.position = transformParent.position;
            bullet.gameObject.SetActive(true);
            bullet.Rotate(zParentRotation);
            bullet.Move();
            yield return new WaitForSeconds(weaponStats.shootingRate);
            if (bulletCountCurrent == 0)
            {
                yield return Reload();
            }
            StartCoroutine(base.Shoot(mousePos, transformParent, callback));
            callback?.Invoke();
        }

        #endregion public void
    }
}