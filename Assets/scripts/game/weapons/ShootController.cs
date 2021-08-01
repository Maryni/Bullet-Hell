using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using Global.Shooting.BulletSpace;
using Global.Shooting;
using Global.Controllers;

public class ShootController : MonoBehaviour
{
    #region private variables

#pragma warning disable
    [SerializeField] private BaseWeapon baseWeapon;
    [SerializeField] private int weaponType = 0;
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private BulletManager bulletPool;
#pragma warning restore

    #endregion private variables

    #region properties

    public int WeaponType => weaponType;

    #endregion properties

    #region public void

    public void ChangeWeaponType(int typeWeapon)
    {
        if (typeWeapon >= 0 && typeWeapon < 4)
        {
            weaponType = typeWeapon;
        }
        else
            Debug.LogError("uncorrect typeWeapon");
    }

    #endregion public void

    #region private void

    private void GetReadyShootByWeapon(int weaponType)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            baseWeapon.GetBullet(bulletPool.GetObject(weaponType));
            baseWeapon.Shot(mousePos);
            return;
        }
    }

    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetReadyShootByWeapon(weaponType);
    }

    #endregion private void
}