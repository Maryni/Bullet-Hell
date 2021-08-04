using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Bullet;
using System.Linq;
using Global.Shooting;
using System;

namespace Global.Managers.Datas
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private List<BaseBullet> poolAutomaticList;
        [SerializeField] private List<BaseBullet> poolShotGunList;
        [SerializeField] private List<BaseBullet> poolRocketList;
        [SerializeField] private AutomaticalBullet automaticalBulletPrefab;
        [SerializeField] private ShotgunBullet shotgunBulletPrefab;
        [SerializeField] private RocketLaucherBullet rocketLaucherBulletPrefab;
        [SerializeField] private Transform transforAutomatic;
        [SerializeField] private Transform transformShotgun;
        [SerializeField] private Transform transformRocket;

        private void Start()
        {
            Init();
        }

        public BaseBullet GetObject(WeaponType weaponType)
        {
            List<BaseBullet> tempListBaseBullets = new List<BaseBullet>();

            switch (weaponType)
            {
                case WeaponType.AutomaticGun:
                    tempListBaseBullets = poolAutomaticList;
                    break;

                case WeaponType.Shotgun:
                    tempListBaseBullets = poolShotGunList;
                    break;

                case WeaponType.RocketLaucher:
                    tempListBaseBullets = poolRocketList;
                    break;
            }
            var findedObj = tempListBaseBullets.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
            if (findedObj == null)
            {
                var newBullet = Instantiate(tempListBaseBullets[0].GetComponent<BaseBullet>(), transform);
                tempListBaseBullets.Add(newBullet);
                return newBullet;
            }
            return findedObj;
        }

        private void Init()
        {
            InitPoolByBullet(poolAutomaticList, automaticalBulletPrefab, transforAutomatic);
            InitPoolByBullet(poolShotGunList, shotgunBulletPrefab, transformShotgun);
            InitPoolByBullet(poolRocketList, rocketLaucherBulletPrefab, transformRocket);
        }

        private void InitPoolByBullet(List<BaseBullet> pool, BaseBullet bulletPrefab, Transform parentTransform)
        {
            for (int i = 0; i < 20; i++)
            {
                pool.Add(Instantiate(bulletPrefab, parentTransform));
                pool[i].gameObject.SetActive(false);
            }
        }
    }
}