using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGun : MonoBehaviour, IWeapon
{
    #region private variables

    [SerializeField] private ShootController shootManager;

    [Header("Not change 0 if we have to load settings")]
    [SerializeField] private int bulletCount;

    [SerializeField] private int currentBullets;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float damage;
    [SerializeField] private float shotingRate;
    [SerializeField] private int bulletOnShotUsed;
    [Space, SerializeField] private WeaponSettings weaponSettings;
    private bool isReloading;

    [SerializeField] private Bullet bullet;

    #endregion private variables

    #region properties

    public int BulletsCount => bulletCount;
    public float CooldownTime => cooldownTime;
    public float Damage => damage;
    public float ShootingRate => shotingRate;
    public int BulletOnShotUsed => bulletOnShotUsed;
    public WeaponSettings WeaponSettings => weaponSettings;
    public Bullet Bullet => bullet;

    #endregion properties

    #region public void

    public void GetBullet(GameObject bulletObject)
    {
        bullet = bulletObject.GetComponent<Bullet>();
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
        bullet.BulletObject.SetActive(true);
        if (currentBullets <= 0)
        {
            Reload();
        }
        if (!isReloading)
            bullet.Move(mousePos);
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
        currentBullets = bulletCount;
    }

    /// <summary>
    /// if we didn't write settings in Inspector
    /// </summary>

    #endregion private void
}