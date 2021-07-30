using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class ShootController : MonoBehaviour
{
    #region private variables

    [SerializeField] private AutomaticGun automaticGun;
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private RocketLaucher rocketLaucher;
    [SerializeField] private int weaponType = 0;
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private BulletPool bulletPool;

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
        if (weaponType == 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                automaticGun.GetBullet(bulletPool.GetObject(weaponType));
                automaticGun.Shot(mousePos);
                return;
            }
        }
        if (weaponType == 1)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                shotgun.GetBullet(bulletPool.GetObject(weaponType));
                shotgun.Shot(mousePos);
                return;
            }
        }
        if (weaponType == 2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                rocketLaucher.GetBullet(bulletPool.GetObject(weaponType));
                rocketLaucher.Shot(mousePos);
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetReadyShootByWeapon(weaponType);
    }

    #endregion private void
}