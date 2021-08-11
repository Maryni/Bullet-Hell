using Global.ActiveObjects;
using Global.Controllers.Spawn;
using Global.Managers;
using Global.Managers.Datas;
using Global.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Controllers
{
    public class SpawnEnemyController : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable

        [SerializeField] private int countSpawnEnemyOnStart;
        [SerializeField] private float timerSpawnEnemy;

#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            GetEnemy(countSpawnEnemyOnStart);
            StartCoroutine(SpawnEnemyByTimeByCount(timerSpawnEnemy, countSpawnEnemyOnStart));
        }

        #endregion Unity functions

        #region private void

        private IEnumerator SpawnEnemyByTimeByCount(float time, int countSpawnPerTime)
        {
            for (int i = 0; i < countSpawnPerTime; i++)
            {
                var tempObject = Services.GetManager<PoolManager>().EnemyPool.GetObject((EnemyType)UnityEngine.Random.Range(0, 3));
                tempObject.gameObject.SetActive(true);
                tempObject.GetComponent<EnemyController>().ActivateEnemy();
            }
            yield return new WaitForSeconds(time);
            yield return SpawnEnemyByTimeByCount(time, countSpawnPerTime);
        }

        private void GetEnemy(int timesRepeat)
        {
            for (int i = 0; i < timesRepeat; i++)
            {
                var tempObject = Services.GetManager<PoolManager>().EnemyPool.GetObject((EnemyType)UnityEngine.Random.Range(0, 3));
                tempObject.gameObject.SetActive(true);
            }
        }

        #endregion private void
    }
}