using Global.ActiveObjects;
using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Global.Managers.Datas
{
    public class EnemyPool : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private List<BaseEnemy> listEnemyPrefabs; //+ list PowerUps
        [SerializeField] private Transform enemyPool;
        [SerializeField] private List<BaseEnemy> listEnemies;
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            Init();
        }

        #endregion Unity functions

        #region public void

        public BaseEnemy GetObject(EnemyType enemyType)
        {
            var findedObj = listEnemies.FirstOrDefault(x => x.EnemyStats.enemyType == enemyType && !x.gameObject.activeInHierarchy);
            if (findedObj == null)
            {
                var newEnemy = Instantiate(listEnemyPrefabs.FirstOrDefault(x => x.GetComponent<BaseEnemy>().EnemyStats.enemyType == enemyType).GetComponent<BaseEnemy>(), enemyPool);
                listEnemies.Add(newEnemy);
                return newEnemy;
            }
            return findedObj;
        }

        public EnemyType GetRandomEnemyType() => (EnemyType)UnityEngine.Random.Range(0, 3);

        public int GetLengthEnemySpawned() => listEnemies.Count;

        public int GetLengthEnemyPrefabs() => listEnemyPrefabs.Count;

        #endregion public void

        #region private void

        private void Init()
        {
            for (int i = 0; i < listEnemyPrefabs.Count; i++)
            {
                InitEnemyInPool(listEnemies, listEnemyPrefabs[i], enemyPool);
            }
        }

        private void InitEnemyInPool(List<BaseEnemy> pool, BaseEnemy enemyPrefab, Transform parentTransform)
        {
            for (int i = 0; i < 20; i++)
            {
                var tempObject = Instantiate(enemyPrefab, parentTransform);
                pool.Add(tempObject);
                tempObject.gameObject.SetActive(false);
            }
        }

        #endregion private void
    }
}