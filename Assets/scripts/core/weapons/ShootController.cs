using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using Global.Shooting.BulletSpace;
using Global.Shooting;
using Global.Controllers;

public class ShootController : MonoBehaviour
{
    #region Inspector variables

#pragma warning disable

    [SerializeField] private BaseWeapon baseWeapon;
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private Transform bulletSaver;
    [SerializeField] private Vector2 mousePos;

#pragma warning restore

    #endregion Inspector variables

    #region properties

    public BaseWeapon CurrentWeapon => baseWeapon;

    #endregion properties

    #region private void

    private void GetReadyShootByWeapon()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            baseWeapon.Shoot(mousePos, bulletSaver, cannonTransform);
        }
    }

    private Quaternion Rotation(Vector3 mousePos, Transform transformObject)
    {
        float AngleRad = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x);
        AngleRad = (180 / Mathf.PI) * AngleRad;
        AngleRad += 90;
        AngleRad += 180;
        return transformObject.rotation = Quaternion.Euler(0, 0, AngleRad);
    }

    #region Unity function

    private void Start()
    {
        cannonTransform = bulletSaver;
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cannonTransform.rotation = Rotation(mousePos, cannonTransform);
        GetReadyShootByWeapon();
    }

    #endregion Unity function

    #endregion private void
}