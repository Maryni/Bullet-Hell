using Global;
using Global.Managers;
using Global.Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGunWeapon : BaseWeapon
{
    private void Start()
    {
        Init();
    }

    public override IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent)
    {
        var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
        bullet.transform.position = transformParent.position;
        bullet.gameObject.SetActive(true);
        bullet.Move(mousePos, transformCanon);
        yield return new WaitForSeconds(.1f);
    }
}