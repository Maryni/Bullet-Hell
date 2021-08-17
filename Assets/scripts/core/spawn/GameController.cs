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
        [SerializeField] private float camOffset;
        [SerializeField] private int randomMin;
        [SerializeField] private int randomMax;

#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private UnityEngine.Camera cam;
        private float height;
        private float width;
        private int[] arrTwoValues = new int[2] { -1, 1 };

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            cam = UnityEngine.Camera.main;
            GetEnemy(countSpawnEnemy);
            StartCoroutine(SpawnEnemyByTimeByCount(timerSpawnEnemy, countSpawnEnemy));
        }

        #endregion Unity functions

        #region private void

        private void GetWidthAndHeight(GameObject gameObjectSpawned)
        {
            int valuetCorrectorWidth = Random.Range(0, arrTwoValues.Length);
            int valueCorrectorHeight = Random.Range(0, arrTwoValues.Length);
            valuetCorrectorWidth = arrTwoValues[valuetCorrectorWidth];
            valueCorrectorHeight = arrTwoValues[valueCorrectorHeight];
            height = cam.orthographicSize + camOffset;
            width = cam.orthographicSize * cam.aspect + camOffset;
            gameObjectSpawned.transform.position = new Vector2(
                (gameObjectSpawned.transform.position.x + width + Random.Range(randomMin, randomMax)) * valuetCorrectorWidth,
                (gameObjectSpawned.transform.position.y + height + Random.Range(randomMin, randomMax)) * valueCorrectorHeight);
        }

        private IEnumerator SpawnEnemyByTimeByCount(float time, int countSpawnPerTime)
        {
            yield return new WaitForEndOfFrame();
            var tempEnemyPoolObject = Services.GetManager<PoolManager>().EnemyPool;
            for (int i = 0; i < countSpawnPerTime; i++)
            {
                var tempObject = tempEnemyPoolObject.GetObject(tempEnemyPoolObject.GetRandomEnemyType());
                GetWidthAndHeight(tempObject.gameObject);
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
                GetWidthAndHeight(tempObject.gameObject);
                tempObject.gameObject.SetActive(true);
            }
        }

        #endregion private void
    }
}