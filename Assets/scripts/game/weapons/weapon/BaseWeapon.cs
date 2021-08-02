using System.Collections;
using System.Collections.Generic;
using Global.Interfaces.Weapon;
using Global.Shooting.BulletSpace;
using UnityEngine;

namespace Global.Shooting
{
    public class BaseWeapon : MonoBehaviour, IWeapon
    {
        #region private variables

#pragma warning disable
        [SerializeField] private bool noLoadSettingFromFile;
        [Header("Settings"), Space, SerializeField] private int bulletCount;

        [SerializeField] private int currentBullets;
        [SerializeField] private float cooldownTime;
        [SerializeField] private float damage;
        [SerializeField] private float shotingRate;
        [SerializeField] private int bulletOnShotUsed;
        [Space, SerializeField] private WeaponSettings weaponSettings;

        [Header("Settings")]
        private bool isReloading;

        [SerializeField] private Bullet bullet;
        private float frameComplete = 0;
        [Header("When 10, then it be only 6 shoot per 1 second if shooting rate = 1"), SerializeField] private int needFramePerRate;
#pragma warning restore

        #endregion private variables

        #region properties

        public WeaponSettings WeaponSettings => weaponSettings;
        int IWeaponStats.BulletsCount => bulletCount;
        float IWeaponStats.CooldownTime => cooldownTime;
        float IWeaponStats.Damage => damage;
        float IWeaponStats.ShootingRate => shotingRate;
        int IWeaponStats.BulletOnShotUsed => bulletOnShotUsed;

        #endregion properties

        #region public void

        public void SetBullet(GameObject bulletObject)
        {
            bullet = bulletObject.GetComponent<Bullet>();
        }

        public void LoadSettings(WeaponSettings weaponSettings)
        {
            if (!noLoadSettingFromFile)
            {
                this.weaponSettings = weaponSettings;

                bulletCount = weaponSettings.BulletsCount;
                cooldownTime = weaponSettings.CooldownTime;
                damage = weaponSettings.Damage;
                shotingRate = weaponSettings.ShootingRate;
                bulletOnShotUsed = weaponSettings.BulletOnShotUsed;
            }
            if (noLoadSettingFromFile)
            {
                if (bulletCount == 0)
                {
                    bulletCount = weaponSettings.BulletsCount;
                }
                if (cooldownTime == 0)
                {
                    cooldownTime = weaponSettings.CooldownTime;
                }
                if (damage == 0)
                {
                    damage = weaponSettings.Damage;
                }
                if (shotingRate == 0)
                {
                    shotingRate = weaponSettings.ShootingRate;
                }
                if (bulletOnShotUsed == 0)
                {
                    bulletOnShotUsed = weaponSettings.BulletOnShotUsed;
                }
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
            bullet.gameObject.SetActive(true);
            if (currentBullets <= 0)
            {
                Reload();
            }
            if (!isReloading && IsCanShoot() && bullet != null)
            {
                bullet.Move(mousePos);
                bullet = null;
            }

            currentBullets--;
        }

        public bool IsValuesChanged() => (bulletCount == 0 && cooldownTime == 0 && damage == 0 && shotingRate == 0);

        #endregion public void

        #region private void

        #region Unity function

        private void Start()
        {
            currentBullets = bulletCount;
        }

        private void FixedUpdate()
        {
            frameComplete += shotingRate;
        }

        #endregion Unity function

        private bool IsCanShoot()
        {
            if (frameComplete > needFramePerRate)
            {
                frameComplete = 0;
                return true;
            }
            return false;
        }

        #endregion private void
    }
}