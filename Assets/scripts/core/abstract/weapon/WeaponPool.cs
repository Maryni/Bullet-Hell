using Global.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Global.Managers.Datas
{
    public class WeaponPool : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private WeaponChanger gameObjectPrefab;
        [SerializeField] private List<WeaponChanger> listWeapons;
        [SerializeField] private Transform transformObjectToSpawn;
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            Init();
        }

        #endregion Unity functions

        #region public void

        public WeaponType GetRandomWeaponType() => (WeaponType)UnityEngine.Random.Range(0, GetWeaponTypeLength());

        public int GetWeaponTypeLength() => Enum.GetValues(typeof(WeaponType)).Length;

        public void DisableWeapons()
        {
            for (int i = 0; i < listWeapons.Count; i++)
            {
                if (listWeapons[i].gameObject.activeInHierarchy)
                {
                    listWeapons[i].gameObject.SetActive(false);
                }
            }
        }

        public WeaponChanger GetObject()
        {
            var findedObj = listWeapons.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
            if (findedObj == null)
            {
                var newObj = Instantiate(gameObjectPrefab, transformObjectToSpawn);
                listWeapons.Add(newObj);
                return newObj;
            }
            return findedObj;
        }

        #endregion public void

        #region private void

        private void Init()
        {
            InitWeaponInPool(listWeapons, gameObjectPrefab, transformObjectToSpawn);
        }

        private void InitWeaponInPool(List<WeaponChanger> pool, WeaponChanger weaponPrefab, Transform parentTransform)
        {
            for (int i = 0; i < 10; i++)
            {
                var tempObject = Instantiate(weaponPrefab, parentTransform);
                pool.Add(tempObject);
                tempObject.SetWeaponRandom();
                tempObject.gameObject.SetActive(false);
            }
        }

        #endregion private void
    }
}