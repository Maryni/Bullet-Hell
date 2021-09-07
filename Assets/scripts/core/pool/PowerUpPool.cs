using Global.Upgrates;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Global.Managers.Datas
{
    public enum TypePowerUp
    {
        IncreaseDamage,
        IncreaseDefence,
        IncreaseSpeed,
        KillEnemies
    }

    public class PowerUpPool : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private PowerUp powerUpPrefab;
        [SerializeField] private List<PowerUp> listPowerUps;
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

        public PowerUp GetObject()
        {
            var findedObj = listPowerUps.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
            if (findedObj == null)
            {
                var newObj = Instantiate(powerUpPrefab, transformObjectToSpawn);
                listPowerUps.Add(newObj);
                return newObj;
            }
            return findedObj;
        }

        public void DisablePowerUps()
        {
            for (int i = 0; i < listPowerUps.Count; i++)
            {
                if (listPowerUps[i].gameObject.activeInHierarchy)
                {
                    listPowerUps[i].gameObject.SetActive(false);
                }
            }
        }

        public TypePowerUp GetRandomPowerUp()
        {
            return (TypePowerUp)UnityEngine.Random.Range(0, GetTypePowerUpLength());
        }

        public int GetTypePowerUpLength()
        {
            return Enum.GetValues(typeof(TypePowerUp)).Length;
        }

        #endregion public void

        #region private void

        private void Init()
        {
            InitPowerUpInPool(listPowerUps, powerUpPrefab, transformObjectToSpawn);
        }

        private void InitPowerUpInPool(List<PowerUp> pool, PowerUp powerUpPrefab, Transform parentTransform)
        {
            for (int i = 0; i < countSpawn; i++)
            {
                var tempObject = Instantiate(powerUpPrefab, parentTransform);
                pool.Add(tempObject);
                tempObject.gameObject.SetActive(false);
            }
        }

        #endregion private void
    }
}