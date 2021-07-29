using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IWeaponStats
{
    #region private variables

    [SerializeField] private int bulletCount;
    [SerializeField] private int currentBullets;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float damage;
    [SerializeField] private float shotingRate;
    private bool isReloading;

    private Transform shotgunLocalTransformBullet;
    [SerializeField] private Bullet bullet;

    #endregion private variables

    #region properties

    public int BulletsCount => bulletCount;

    public float CooldownTime => cooldownTime;

    public float Damage => damage;

    public float ShootingRate => shotingRate;

    #endregion properties

    #region public void

    public void Reload()
    {
        float timer = cooldownTime;
        isReloading = true;
        while (timer > 0)
        {
            timer -= 0.1f;
            Debug.Log("Reloading " + this.gameObject.name);
        }
        if (timer < 0)
        {
            isReloading = false;
            currentBullets = bulletCount;
        }
        Debug.Log("Reloading finished " + this.gameObject.name);
    }

    public void Shot(Vector2 mousePos)
    {
        bullet.BulletObject.SetActive(true);
        if (currentBullets == 0)
        {
            Reload();
        }
        bullet.Move(mousePos);
        currentBullets--;
    }

    #endregion public void

    #region private void

    private void Start()
    {
        shotgunLocalTransformBullet = bullet.BulletObject.transform;
        bullet.GetBackupTransform(shotgunLocalTransformBullet);
    }

    #endregion private void
}