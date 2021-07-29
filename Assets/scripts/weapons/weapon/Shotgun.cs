using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IWeapon

{
    #region private variables

    [SerializeField] private int bulletCount;
    [SerializeField] private int currentBullets;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float damage;
    [SerializeField] private float shotingRate;
    [SerializeField] private int bulletOnShotUsed;
    [SerializeField] private WeaponSettings weaponSettings;
    private bool isReloading;
    [SerializeField] private int countBulletToShotByOneTime;

    private Transform shotgunLocalTransformBullet;
    [SerializeField] private List<Bullet> bulletsToShoot;

    #endregion private variables

    #region properties

    public int BulletsCount => bulletCount;
    public float CooldownTime => cooldownTime;
    public float Damage => damage;
    public float ShootingRate => shotingRate;
    public int BulletOnShotUsed => bulletOnShotUsed;
    public WeaponSettings WeaponSettings => weaponSettings;

    #endregion properties

    #region public void

    public void GetBullet(GameObject bulletObject)
    {
        for (int i = bulletsToShoot.Count; i < countBulletToShotByOneTime; i++)
        {
            bulletsToShoot.Add(bulletObject.GetComponent<Bullet>());
        }
    }

    public void LoadSettings()
    {
        if (!IsValuesChanged())
        {
            bulletCount = WeaponSettings.BulletsCount;
            cooldownTime = WeaponSettings.CooldownTime;
            damage = WeaponSettings.Damage;
            shotingRate = WeaponSettings.ShootingRate;
        }
    }

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
        for (int i = 0; i < bulletsToShoot.Count; i++)
        {
            bulletsToShoot[i].BulletObject.SetActive(true);
        }
        if (currentBullets <= 0)
        {
            Reload();
        }
        if (!isReloading)
        {
            for (int i = 0; i < bulletsToShoot.Count; i++)
            {
                bulletsToShoot[i].Move(mousePos, new Vector3(1f, Random.Range(-2f, 2f), 1f));
            }
            bulletsToShoot.Clear();
        }

        currentBullets--;
    }

    public bool IsValuesChanged()
    {
        if (bulletCount == 0 && cooldownTime == 0 && damage == 0 && shotingRate == 0)
            return false;
        else
            return true;
    }

    #endregion public void

    #region private void

    private void Start()
    {
        LoadSettings();
        //shotgunLocalTransformBullet = bullet.BulletObject.transform;
        currentBullets = bulletCount;
    }

    #endregion private void
}