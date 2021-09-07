using Global.Player;
using Global.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Global.Managers.Datas
{
    public class WeaponPool : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private WeaponChanger weaponSpawningPrefab;
        [SerializeField] private List<WeaponChanger> listWeapons;
        [SerializeField] private Transform transformObjectToSpawn;
        [SerializeField] private int countSpawn;
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            Init();
        }

        #endregion Unity functions

        #region public void

        public WeaponType GetWeaponTypeExclusivePlayerWeapon()
        {
            var currentType = FindObjectOfType<PlayerController>().GetWeaponTypeByPlayer();
            List<WeaponType> weaponTypes = new List<WeaponType>();
            for (int i = 0; i < Enum.GetValues(typeof(WeaponType)).Length; i++)
            {
                weaponTypes.Add((WeaponType)Enum.GetValues(typeof(WeaponType)).GetValue(i));
            }
            weaponTypes.Remove(currentType);
            return weaponTypes[UnityEngine.Random.Range(0, weaponTypes.Count)];
        }

        public WeaponType GetAllRandomWeaponType()
        {
            return (WeaponType)UnityEngine.Random.Range(0, GetWeaponTypeLength());
        }

        public int GetWeaponTypeLength()
        {
            return Enum.GetValues(typeof(WeaponType)).Length;
        }

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
                var newObj = Instantiate(weaponSpawningPrefab, transformObjectToSpawn);
                listWeapons.Add(newObj);
                return newObj;
            }
            return findedObj;
        }

        #endregion public void

        #region private void

        private void Init()
        {
            InitWeaponInPool(listWeapons, weaponSpawningPrefab, transformObjectToSpawn);
        }

        private void InitWeaponInPool(List<WeaponChanger> pool, WeaponChanger weaponPrefab, Transform parentTransform)
        {
            for (int i = 0; i < countSpawn; i++)
            {
                var tempObject = Instantiate(weaponPrefab, parentTransform);
                pool.Add(tempObject);
                tempObject.gameObject.SetActive(false);
            }
        }

        #endregion private void
    }
}