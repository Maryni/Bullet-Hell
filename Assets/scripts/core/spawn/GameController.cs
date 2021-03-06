using Global.ActiveObjects;
using Global.Managers;
using Global.Managers.Datas;
using Global.Player;
using Global.Upgrates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Controllers
{
    public class GameController : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private GameObject plane;
        [SerializeField] private int countSpawnEnemy;
        [SerializeField] private float timerSpawnEnemy;
        [SerializeField] private float camOffset;

#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private PlayerController playerController;
        private Dictionary<TypePowerUp, Action> values = new Dictionary<TypePowerUp, Action>();
        private UnityEngine.Camera cam;
        private float height;
        private float width;
        private int[] arrTwoValues = new int[2] { -1, 1 };
        private int timerSpawnWeapon;
        private int timerDispawnWeapon;
        private int timerSpawnPowerUp;
        private int timerDispawnPowerUp;
        private float timerDuration;
        private PowerUp tempObject;
        private Coroutine coroutineDamage;
        private Coroutine coroutineSpeed;

        #endregion private variables

        #region Unity functions

        private void Awake()
        {
            SetValuesToDictionary();
        }

        private void Start()
        {
            if (cam == null)
            {
                cam = UnityEngine.Camera.allCameras[0];
            }
            playerController = FindObjectOfType<PlayerController>();

            SetTimersFromData();
            StartCoroutine(SpawnEnemyByTimeByCount(timerSpawnEnemy, countSpawnEnemy));
            StartCoroutine(SpawnWeaponByTime(timerSpawnWeapon));
            StartCoroutine(SpawnPowerUpByTime(timerSpawnPowerUp));
        }

        #endregion Unity functions

        #region public void

        public void SetPowerUpByType(TypePowerUp typePowerUp, PowerUp gameObjectUsedMethod)
        {
            tempObject = gameObjectUsedMethod;
            values[typePowerUp]?.Invoke();
            tempObject.gameObject.SetActive(false);
            tempObject = null;
        }

        public void DisableSpawningEverything()
        {
            DisableSpawningEnemy();
            DisableSpawningGun();
            DisableSpawningPowerUp();
            //StopAllCoroutines();
        }

        public void DisableSpawnedItems()
        {
            DisableEnemies();
            DisableBullets();
            DisableWeapons();
            DisablePowerUps();
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

        private void DisableSpawningPowerUp()
        {
            StopCoroutine(SpawnPowerUpByTime(timerSpawnPowerUp));
        }

        private void DisableEnemies()
        {
            Services.GetManager<PoolManager>().EnemyPool.DisableEnemies();
        }

        private void DisableBullets()
        {
            Services.GetManager<PoolManager>().BulletPool.DisableBullets();
        }

        private void DisableWeapons()
        {
            Services.GetManager<PoolManager>().WeaponPool.DisableWeapons();
        }

        private void DisablePowerUps()
        {
            Services.GetManager<PoolManager>().PowerUpPool.DisablePowerUps();
        }

        private void SetValuesToDictionary()
        {
            values.Add(TypePowerUp.IncreaseDamage, () => DamagePowerUp());
            values.Add(TypePowerUp.IncreaseDefence, () => DefencePowerUp());
            values.Add(TypePowerUp.IncreaseSpeed, () => SpeedPowerUp());
            values.Add(TypePowerUp.KillEnemies, () => KillAreaPowerUp());
        }

        private void DamagePowerUp()
        {
            if (coroutineDamage == null)
            {
                coroutineDamage = StartCoroutine(DamagePowerUpChanging());
            }
        }

        private void SpeedPowerUp()
        {
            if (coroutineSpeed == null)
            {
                coroutineSpeed = StartCoroutine(SpeedPowerUpChanging());
            }
        }

        private void DefencePowerUp()
        {
            playerController.EnableImmortalityOnTime(timerDuration);
        }

        private IEnumerator DamagePowerUpChanging()
        {
            Services.GetManager<DataManager>().DynamicData.EnablesPowerUpByType(TypePowerUp.IncreaseDamage, true);
            playerController.RefreshPlayerStatsFromData();
            yield return new WaitForSeconds(timerDuration);
            Services.GetManager<DataManager>().DynamicData.EnablesPowerUpByType(TypePowerUp.IncreaseDamage, false);
            playerController.RefreshPlayerStatsFromData();
            coroutineDamage = null;
        }

        private IEnumerator SpeedPowerUpChanging()
        {
            Services.GetManager<DataManager>().DynamicData.EnablesPowerUpByType(TypePowerUp.IncreaseSpeed, true);
            playerController.RefreshPlayerStatsFromData();
            yield return new WaitForSeconds(timerDuration);
            Services.GetManager<DataManager>().DynamicData.EnablesPowerUpByType(TypePowerUp.IncreaseSpeed, false);
            playerController.RefreshPlayerStatsFromData();
            coroutineSpeed = null;
        }

        private void KillAreaPowerUp()
        {
            tempObject.IncreaseRadius();
        }

        private void SetTimersFromData()
        {
            var data = Services.GetManager<DataManager>();
            timerSpawnWeapon = data.DynamicData.WeaponSpawnItemData.spawnTime;
            timerDispawnWeapon = data.DynamicData.WeaponSpawnItemData.destroyTime;
            timerSpawnPowerUp = data.DynamicData.PowerUpSpawnItemData.spawnTime;
            timerDispawnPowerUp = data.DynamicData.PowerUpSpawnItemData.destroyTime;
            timerDuration = data.DynamicData.PowerUpSpawnItemData.duration;
        }

        private void GetWidthAndHeightForSpawnWithoutCameraView(GameObject gameObjectSpawned)
        {
            int valuetCorrectorWidth = UnityEngine.Random.Range(0, arrTwoValues.Length);
            int valueCorrectorHeight = UnityEngine.Random.Range(0, arrTwoValues.Length);
            valuetCorrectorWidth = arrTwoValues[valuetCorrectorWidth];
            valueCorrectorHeight = arrTwoValues[valueCorrectorHeight];
            height = cam.orthographicSize + camOffset;
            width = cam.orthographicSize * cam.aspect + camOffset;
            gameObjectSpawned.transform.position = new Vector2(
                (UnityEngine.Random.Range(width, plane.GetComponent<Collider2D>().bounds.size.x / 2)) * valuetCorrectorWidth,
                (UnityEngine.Random.Range(height, plane.GetComponent<Collider2D>().bounds.size.y / 2)) * valueCorrectorHeight);
        }

        private void GetWidthAndHeightForSpawnInCameraView(GameObject gameObject)
        {
            float camHeight = UnityEngine.Random.Range(cam.ScreenToWorldPoint(new Vector2(0, 0)).y, cam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float camWidth = UnityEngine.Random.Range(cam.ScreenToWorldPoint(new Vector2(0, 0)).x, cam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            Vector3 spawnPoint = plane.GetComponent<Collider2D>().bounds.ClosestPoint(new Vector2(camWidth, camHeight));

            gameObject.transform.position = spawnPoint;
        }

        private IEnumerator SpawnWeaponByTime(int timesRepeat)
        {
            yield return new WaitForEndOfFrame();
            var tempWeaponPool = Services.GetManager<PoolManager>().WeaponPool;
            var tempObject = tempWeaponPool.GetObject();
            tempObject.gameObject.SetActive(true);
            tempObject.ResetCoroutine();
            tempObject.CheckAndSetPlayerTransform();
            tempObject.SetWeaponRandom();
            tempObject.SetSprite();
            GetWidthAndHeightForSpawnInCameraView(tempObject.gameObject);
            tempObject.DisableObjectByTime(timerDispawnWeapon);
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

        private IEnumerator SpawnPowerUpByTime(int timesRepeat)
        {
            yield return new WaitForEndOfFrame();
            var tempPowerUpPoolObject = Services.GetManager<PoolManager>().PowerUpPool;
            var tempObject = tempPowerUpPoolObject.GetObject();
            tempObject.gameObject.SetActive(true);
            tempObject.CheckAndSetGameController();
            tempObject.SetPowerUpType(tempPowerUpPoolObject.GetRandomPowerUp());
            tempObject.SetSprite();
            GetWidthAndHeightForSpawnInCameraView(tempObject.gameObject);
            tempObject.DispawnObjectByTime(timerDispawnPowerUp);
            yield return new WaitForSeconds(timesRepeat);
            yield return SpawnPowerUpByTime(timesRepeat);
        }

        #endregion private void
    }
}