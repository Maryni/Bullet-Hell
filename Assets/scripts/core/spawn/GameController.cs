using Global.ActiveObjects;
using Global.Managers;
using Global.Managers.Datas;
using System.Collections;
using UnityEngine;

namespace Global.Controllers
{
    public class GameController : MonoBehaviour
    {
        #region Inspector variables

        //Add spawning random when camera not see enemy

#pragma warning disable
        [SerializeField] private GameObject plane;
        [SerializeField] private int countSpawnEnemy;
        [SerializeField] private float timerSpawnEnemy;
        [SerializeField] private float camOffset;

#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private UnityEngine.Camera cam;
        private float height;
        private float width;
        private int[] arrTwoValues = new int[2] { -1, 1 };
        private int timerSpawnWeapon;
        private int timerDispawnWeapon;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            if (cam == null)
            {
                cam = UnityEngine.Camera.allCameras[0];
            }
            SetTimersFromData();
            StartCoroutine(SpawnEnemyByTimeByCount(timerSpawnEnemy, countSpawnEnemy));
            StartCoroutine(SpawnWeaponByTime(timerSpawnWeapon));
        }

        #endregion Unity functions

        #region public void

        public void DisableSpawningEverything()
        {
            DisableSpawningEnemy();
            DisableSpawningGun();
        }

        public void DisableSpawnedItems()
        {
            DisableEnemies();
            DisableBullets();
        }

        #endregion public void

        #region private void

        private void DisableSpawningEnemy()
        {
            StopCoroutine(SpawnEnemyByTimeByCount(timerSpawnEnemy, countSpawnEnemy));
        }

        private void DisableSpawningGun()
        {
            StopCoroutine(SpawnWeaponByTime(timerSpawnWeapon));
        }

        private void DisableEnemies()
        {
            Services.GetManager<PoolManager>().EnemyPool.DisableEnemies();
        }

        private void DisableBullets()
        {
            Services.GetManager<PoolManager>().BulletPool.DisableBullets();
        }

        private void SetTimersFromData()
        {
            var data = Services.GetManager<DataManager>();
            timerSpawnWeapon = data.DynamicData.SpawnItemData.spawnTime;
            timerDispawnWeapon = data.DynamicData.SpawnItemData.destroyTime;
        }

        private void GetWidthAndHeightForSpawnWithoutCameraView(GameObject gameObjectSpawned)
        {
            int valuetCorrectorWidth = Random.Range(0, arrTwoValues.Length);
            int valueCorrectorHeight = Random.Range(0, arrTwoValues.Length);
            valuetCorrectorWidth = arrTwoValues[valuetCorrectorWidth];
            valueCorrectorHeight = arrTwoValues[valueCorrectorHeight];
            height = cam.orthographicSize + camOffset;
            width = cam.orthographicSize * cam.aspect + camOffset;
            gameObjectSpawned.transform.position = new Vector2(
                (Random.Range(width, plane.GetComponent<Collider2D>().bounds.size.x / 2)) * valuetCorrectorWidth,
                (Random.Range(height, plane.GetComponent<Collider2D>().bounds.size.y / 2)) * valueCorrectorHeight);
        }

        //Переписывай, Миша
        //тут ифов больше, чем крупинок соли в щепотке

        // private void GetWidthAndHeightForSpawnInCameraView(GameObject gameObject)
        // {
        //     float minX, minY, maxX, maxY;
        //     minX = -plane.GetComponent<Collider2D>().bounds.size.x / 2;
        //     maxX = -minX;
        //     minY = -plane.GetComponent<Collider2D>().bounds.size.y / 2;
        //     maxY = -minY;
        //     float spawnX, spawnY;
        //     float camHeight = Random.Range(cam.ScreenToWorldPoint(new Vector2(0, 0)).y, cam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        //     float camWidth = Random.Range(cam.ScreenToWorldPoint(new Vector2(0, 0)).x, cam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        //     float pointRangeXMin = 0, pointRangeXMax = 0, pointRangeYMin = 0, pointRangeYMax = 0, pointXDefault = 0, pointYDefault = 0;
        //     if (camHeight > maxY)
        //     {
        //         pointRangeYMax = maxY;
        //     }
        //     else if (camHeight < minY)
        //     {
        //         pointRangeYMin = minY;
        //     }
        //     else
        //     {
        //         pointYDefault = camHeight;
        //     }
        //
        //     if (camWidth > maxX)
        //     {
        //         pointRangeXMax = maxX;
        //     }
        //     else if (camWidth < minX)
        //     {
        //         pointRangeXMin = minX;
        //     }
        //     else
        //     {
        //         pointXDefault = camWidth;
        //     }
        //
        //     if (pointRangeXMin == 0)
        //     {
        //         if (pointRangeXMax == 0)
        //         {
        //             spawnX = Random.Range
        //         (camWidth,
        //         pointXDefault);
        //         }
        //         else
        //         {
        //             spawnX = Random.Range
        //         (camWidth,
        //         pointRangeXMax);
        //         }
        //     }
        //     else
        //     {
        //         if (pointRangeXMax == 0)
        //         {
        //             spawnX = Random.Range
        //         (pointRangeXMin,
        //         pointXDefault);
        //         }
        //         else
        //         {
        //             spawnX = Random.Range
        //         (pointRangeXMin,
        //         pointRangeXMax);
        //         }
        //     }
        //
        //     if (pointRangeYMin == 0)
        //     {
        //         if (pointRangeYMax == 0)
        //         {
        //             spawnY = Random.Range
        //         (camHeight,
        //         pointYDefault);
        //         }
        //         else
        //         {
        //             spawnY = Random.Range
        //         (camHeight,
        //         pointRangeYMax);
        //         }
        //     }
        //     else
        //     {
        //         if (pointRangeYMax == 0)
        //         {
        //             spawnY = Random.Range
        //         (pointRangeYMin,
        //         pointYDefault);
        //         }
        //         else
        //         {
        //             spawnY = Random.Range
        //         (pointRangeYMin,
        //         pointRangeYMax);
        //         }
        //     }
        //
        //     gameObject.transform.position = new Vector2(spawnX, spawnY);
        // }

        private IEnumerator SpawnWeaponByTime(int timesRepeat)
        {
            yield return new WaitForEndOfFrame();
            var tempWeaponPool = Services.GetManager<PoolManager>().WeaponPool;
            var tempObject = tempWeaponPool.GetObject();
            tempObject.gameObject.SetActive(true);
            tempObject.SetWeaponRandom();
            GetWidthAndHeightForSpawnInCameraView(tempObject.gameObject);
            StartCoroutine(tempObject.DisableObjectByTime(timerDispawnWeapon));
            yield return new WaitForSeconds(timesRepeat);
            yield return SpawnWeaponByTime(timesRepeat);
        }

        private IEnumerator SpawnEnemyByTimeByCount(float time, int countSpawnPerTime)
        {
            yield return new WaitForEndOfFrame();
            var tempEnemyPoolObject = Services.GetManager<PoolManager>().EnemyPool;
            for (int i = 0; i < countSpawnPerTime; i++)
            {
                var tempObject = tempEnemyPoolObject.GetObject(tempEnemyPoolObject.GetRandomEnemyType());
                GetWidthAndHeightForSpawnWithoutCameraView(tempObject.gameObject);
                tempObject.gameObject.SetActive(true);
                tempObject.GetComponent<EnemyController>().ActivateEnemy();
            }
            yield return new WaitForSeconds(time);
            yield return SpawnEnemyByTimeByCount(time, countSpawnPerTime);
        }

        #endregion private void
    }
}