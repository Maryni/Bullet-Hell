using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
public class ShootManager : MonoBehaviour
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

    public int WeaponType => weaponType; //не используеться - не инкапсулируй переменную

    #endregion properties

    #region public void

    //функцию можно сломать проще некуда
    //Важное причемание: перепиши логику оружия
    public void ChangeWeaponType(int typeWeapon)
    {
        if (typeWeapon >= 0)
        {
            weaponType = typeWeapon;
        }
    }

    #endregion public void

    #region private void

    //DRY
    //KISS
    //А что если у нас будет 10000 типов оружий, ты будешь через иф расписывать каждый из них? :)
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

    //нажатие мышки может произойти вне фиксед апдейта - в итоге игрок не выстрелит
    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetReadyShootByWeapon(weaponType);
    }

    #endregion private void
}