using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Global.Shooting.BulletSpace
{
    public class BulletManager : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private Transform[] parentTransforms;
        [SerializeField] private List<GameObject> bulletsType;
        [SerializeField] private GameObject[] bulletsAutomatic;
        [SerializeField] private GameObject[] bulletsShotgun;
        [SerializeField] private GameObject[] bulletsRocket;
        [SerializeField] private List<GameObject[]> bulletsList = new List<GameObject[]>();
        [SerializeField] private int countObjectsInPool;
        [SerializeField] private int countStepAddToArray;
        [SerializeField] private BulletFactory bulletFactory;
#pragma warning restore

        #endregion private variables

        #region properties

        public Transform[] ParentTransforms => parentTransforms;

        #endregion properties

        #region public void

        public void SetTimerToBlowUpRocket(float timer) => bulletsType[2].GetComponent<Bullet>().SetTimer(timer);

        public GameObject GetObject(int indexWeapon)
        {
            if (CheckValue(bulletsList[indexWeapon]))
            {
                bulletsList.Insert(indexWeapon, bulletFactory.SpawnObjectsForFillArray(bulletsList[indexWeapon], parentTransforms[indexWeapon], bulletsType[indexWeapon], bulletsList[indexWeapon].Length + countStepAddToArray));
                bulletsList.RemoveAt(indexWeapon + 1);
            }
            for (int i = 0; i < bulletsList[indexWeapon].Length; i++)
            {
                if (bulletsList[indexWeapon][i].activeInHierarchy == false)
                {
                    bulletsList[indexWeapon][i].GetComponent<Bullet>().GetBackToParent();
                    return bulletsList[indexWeapon][i];
                }
            }

            GetObject(indexWeapon);
            return null;

            bool CheckValue(GameObject[] array)
            {
                bool active = false;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].activeInHierarchy)
                    {
                        active = true;
                    }
                    if (!array[i].activeInHierarchy)
                    {
                        return false;
                    }
                }
                return active;
            }
        }

        #endregion public void

        #region private void

        #region Unity function

        private void Awake()
        {
            SetArraysInBulletsList();
            SetDefaultArrays();
        }

        #endregion Unity function

        private void SetArraysInBulletsList()
        {
            bulletsAutomatic = new GameObject[countObjectsInPool];
            bulletsShotgun = new GameObject[countObjectsInPool];
            bulletsRocket = new GameObject[countObjectsInPool];
            bulletsList.Add(bulletsAutomatic);
            bulletsList.Add(bulletsShotgun);
            bulletsList.Add(bulletsRocket);
        }

        private void SetDefaultArrays()
        {
            for (int i = 0; i < bulletsList.Count; i++)
            {
                bulletsList[i] = bulletFactory.SpawnObjectsForFillArray(bulletsList[i], parentTransforms[i], bulletsType[i]);
            }
        }

        #endregion private void
    }
}