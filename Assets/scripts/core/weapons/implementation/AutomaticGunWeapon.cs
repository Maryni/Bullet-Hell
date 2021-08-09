﻿using Global;
using Global.Managers;
using Global.Shooting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGunWeapon : BaseWeapon
{
    #region private variables

    private bool canShoot;
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

    public override IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent, Action callback = null)
    {
        StartCoroutine(Reload());
        if (canShoot)
        {
            bulletCountCurrent--;
            var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
            bullet.transform.position = transformParent.position;
            bullet.gameObject.SetActive(true);
            bullet.Move(transformCanon);
            yield return new WaitForSeconds(weaponStats.shooringRate);
            callback?.Invoke();
        }
    }

    public override IEnumerator Reload()
    {
        canShoot = true;
        if (bulletCountCurrent <= 0)
        {
            canShoot = false;
            yield return new WaitForSeconds(weaponStats.cooldownTime);
            bulletCountCurrent = weaponStats.bulletCount;
            canShoot = true;
            yield return canShoot;
        }
        yield return canShoot;
    }

    #endregion public void
}