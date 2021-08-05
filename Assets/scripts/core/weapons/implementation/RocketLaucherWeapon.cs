using System;
using Global.Managers;
using Global.Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Weapon
{
    public class RocketLaucherWeapon : BaseWeapon
    {
        private void Start()
        {
            Init();
        }

        public override IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent, Action callback = null)
        {
            var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
            bullet.transform.parent = transformParent;
            bullet.gameObject.SetActive(true);
            bullet.Move(mousePos, transformCanon);
            yield return new WaitForSeconds(.1f);
        }
    }
}