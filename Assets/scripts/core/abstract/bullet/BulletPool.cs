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
        #region Inspector variables

#pragma warning disable
        [SerializeField] private List<BaseBullet> poolAutomaticList;
        [SerializeField] private List<BaseBullet> poolShotGunList;
        [SerializeField] private List<BaseBullet> poolRocketList;
        [SerializeField] private AutomaticalBullet automaticalBulletPrefab;
        [SerializeField] private ShotgunBullet shotgunBulletPrefab;
        [SerializeField] private RocketLaucherBullet rocketLaucherBulletPrefab;
        [SerializeField] private Transform transformAutomatic;
        [SerializeField] private Transform transformShotgun;
        [SerializeField] private Transform transformRocket;
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            Init();
        }

        #endregion Unity functions

        #region public void

        public void DisableBullets()
        {
            for (int i = 0; i < poolAutomaticList.Count; i++)
            {
                if (poolAutomaticList[i].gameObject.activeInHierarchy)
                {
                    poolAutomaticList[i].gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < poolShotGunList.Count; i++)
            {
                if (poolShotGunList[i].gameObject.activeInHierarchy)
                {
                    poolShotGunList[i].gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < poolRocketList.Count; i++)
            {
                if (poolRocketList[i].gameObject.activeInHierarchy)
                {
                    poolRocketList[i].gameObject.SetActive(false);
                }
            }
        }

        public BaseBullet GetObject(WeaponType weaponType)
        {
            List<BaseBullet> tempListBaseBullets = new List<BaseBullet>();
            Transform tempTransformPool = transformAutomatic;

            switch (weaponType)
            {
                case WeaponType.AutomaticGun:
                    tempListBaseBullets = poolAutomaticList;
                    tempTransformPool = transformAutomatic;
                    break;

                case WeaponType.Shotgun:
                    tempListBaseBullets = poolShotGunList;
                    tempTransformPool = transformShotgun;
                    break;

                case WeaponType.RocketLaucher:
                    tempListBaseBullets = poolRocketList;
                    tempTransformPool = transformRocket;
                    break;
            }
            var findedObj = tempListBaseBullets.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
            if (findedObj == null)
            {
                var newBullet = Instantiate(tempListBaseBullets[0].GetComponent<BaseBullet>(), tempTransformPool);
                tempListBaseBullets.Add(newBullet);
                return newBullet;
            }
            return findedObj;
        }

        #endregion public void

        #region private void

        private void Init()
        {
            InitPoolByBullet(poolAutomaticList, automaticalBulletPrefab, transformAutomatic);
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

        #endregion private void
    }
}