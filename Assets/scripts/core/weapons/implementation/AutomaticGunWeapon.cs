using Global;
using Global.Managers;
using Global.Shooting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGunWeapon : BaseWeapon
{
    #region Unity functions

    private void Start()
    {
        Init();
    }

    #endregion Unity functions

    #region public void

    public override IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent, Action callback = null)
    {
        var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
        bullet.transform.position = transformParent.position;
        bullet.gameObject.SetActive(true);
        bullet.Move(transformCanon);
        yield return new WaitForSeconds(.1f);
    }

    #endregion public void
}