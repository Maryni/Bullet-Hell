using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    #region private variables

    [SerializeField] private AutomaticGun automaticGun;
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private RocketLaucher rocketLaucher;
    [SerializeField] private int weaponType = 0;
    [SerializeField] private Vector2 mousePos;

    #endregion private variables

    #region public void

    public void GetReadyShootByWeapon(int weaponType)
    {
        if (weaponType == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                automaticGun.Shot(mousePos);
                return;
            }
        }
        if (weaponType == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                shotgun.Shot(mousePos);
                return;
            }
        }
        if (weaponType == 2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                rocketLaucher.Shot(mousePos);
                return;
            }
        }
    }

    public void ChangeWeaponType(int typeWeapon)
    {
        if (typeWeapon >= 0)
        {
            weaponType = typeWeapon;
        }
    }

    #endregion public void

    #region private void

    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetReadyShootByWeapon(weaponType);
    }

    #endregion private void
}