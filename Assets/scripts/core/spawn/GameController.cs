using Global.ActiveObjects;
using Global.Managers;
using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Controllers
{
    public class GameController : MonoBehaviour
    {
        #region Inspector variables

        //Add spawning random when camera not see enemy

#pragma warning disable

        [SerializeField] private int countSpawnEnemy;
        [SerializeField] private float timerSpawnEnemy;

#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            GetEnemy(countSpawnEnemy);
            StartCoroutine(SpawnEnemyByTimeByCount(timerSpawnEnemy, countSpawnEnemy));
        }

        #endregion Unity functions

        #region private void

        private IEnumerator SpawnEnemyByTimeByCount(float time, int countSpawnPerTime)
        {
            var tempEnemyPoolObject = Services.GetManager<PoolManager>().EnemyPool;
            for (int i = 0; i < countSpawnPerTime; i++)
            {
                var tempObject = tempEnemyPoolObject.GetObject(tempEnemyPoolObject.GetRandomEnemyType());
                tempObject.gameObject.SetActive(true);
                tempObject.GetComponent<EnemyController>().ActivateEnemy();
            }
            yield return new WaitForSeconds(time);
            yield return SpawnEnemyByTimeByCount(time, countSpawnPerTime);
        }

        private void GetEnemy(int timesRepeat)
        {
            var tempEnemyPoolObject = Services.GetManager<PoolManager>().EnemyPool;
            for (int i = 0; i < timesRepeat; i++)
            {
                var tempObject = tempEnemyPoolObject.GetObject(tempEnemyPoolObject.GetRandomEnemyType());
                tempObject.gameObject.SetActive(true);
            }
        }

        #endregion private void
    }
}