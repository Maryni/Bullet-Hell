using Global.ActiveObjects;
using Global.Managers;
using Global.Managers.Datas;
using Global.Weapon;
using System.Collections;
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
        private int timerSpawnWeapon;
        private int timerDispawnWeapon;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            if (cam == null)
            {
                cam = UnityEngine.Camera.main;
            }
            SetTimersFromData();
            StartCoroutine(SpawnEnemyByTimeByCount(timerSpawnEnemy, countSpawnEnemy));
            StartCoroutine(SpawnWeaponByTime(timerSpawnWeapon));
        }

        #endregion Unity functions

        #region public void

        public void DisableEnemies()
        {
            Services.GetManager<PoolManager>().EnemyPool.DisableEnemies();
        }

        public void DisableBullets()
        {
            Services.GetManager<PoolManager>().BulletPool.DisableBullets();
        }

        #endregion public void

        #region private void

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
                (gameObjectSpawned.transform.position.x + width + Random.Range(randomMin, randomMax)) * valuetCorrectorWidth,
                (gameObjectSpawned.transform.position.y + height + Random.Range(randomMin, randomMax)) * valueCorrectorHeight);
        }

        private void GetWidthAndHeightForSpawnInCameraView(GameObject gameObject)
        {
            float spawnY = Random.Range
                (cam.ScreenToWorldPoint(new Vector2(0, 0)).y,
                cam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (cam.ScreenToWorldPoint(new Vector2(0, 0)).x,
                cam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            Vector2 spawnPoint = new Vector2(spawnX, spawnY);
            gameObject.transform.position = spawnPoint;
        }

        private IEnumerator SpawnWeaponByTime(int timesRepeat)
        {
            yield return new WaitForEndOfFrame();
            var tempWeaponPool = Services.GetManager<PoolManager>().WeaponPool;
            var tempObject = tempWeaponPool.GetObject();
            tempObject.gameObject.SetActive(true);
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