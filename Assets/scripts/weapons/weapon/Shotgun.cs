using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IWeapon

{
    #region private variables

    [Header("Not change 0 if we have to load settings")]
    [SerializeField] private int bulletCount;

    [SerializeField] private int currentBullets;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float damage;
    [SerializeField] private float shotingRate;
    [SerializeField] private int bulletOnShotUsed;
    [Space, SerializeField] private WeaponSettings weaponSettings;
    private bool isReloading;
    [SerializeField] private int countBulletToShotByOneTime;

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
                Vector3 angel = Vector3.one;
                if (mousePos.x > 0.25)
                {
                    angel = new Vector3(1f, Random.Range(-2f, 2f), 1f);
                }
                if (mousePos.x < -0.25)
                {
                    angel = new Vector3(1f, Random.Range(-2f, 2f), 1f);
                }
                if (mousePos.y > 0.25)
                {
                    angel = new Vector3(Random.Range(-2f, 2f), 1f, 1f);
                }
                if (mousePos.y < -0.25)
                {
                    angel = new Vector3(Random.Range(-2f, 2f), 1f, 1f);
                }
                bulletsToShoot[i].Move(mousePos, angel);
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
        currentBullets = bulletCount;
    }

    #endregion private void
}