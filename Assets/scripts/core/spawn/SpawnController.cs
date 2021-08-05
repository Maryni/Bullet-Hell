using Global.Controllers.Spawn;
using Global.Managers.Datas;
using Global.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Controllers
{
    public class SpawnController : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private Spawner spawner;
        [SerializeField] private TimerController timer;
        [SerializeField] private bool needToSpawn;
        [SerializeField] private ShootController shootController;
        [SerializeField] private ItemInfo currentItemInfo;
        [SerializeField] private int countSpawnInfo;
        [SerializeField] private List<GameObject> enemyTypeList;

#pragma warning restore

        #endregion private variables

        #region properties

        public Spawner Spawner => spawner;

        #endregion properties

        #region public void

        public void DisableSpawn() => needToSpawn = false;

        public void LoadSettings(float timerValue, float timerToSpawner)
        {
            timer.SetTimerValue(Services.GetManager<DataManager>().DynamicData.SpawnItemData.destroyTime);
            spawner.SetTimer(Services.GetManager<DataManager>().DynamicData.SpawnItemData.spawnTime);
        }

        #endregion public void

        #region private void

        private void SpawnUntilCount(int count)
        {
            if (spawner.SpawnList.Count >= count)
            {
                needToSpawn = false;
            }
            if (timer.GetTimerFinishedRepeating() && needToSpawn)
            {
                spawner.Spawn();
                spawner.Spawn(enemyTypeList[0], count);
            }
        }

        #region Unity function

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Item")
            {
                currentItemInfo = collision.gameObject.GetComponent<ItemInfo>();
                int value = int.Parse(currentItemInfo.GetValue());
                //shootController.ChangeWeaponType(value);
                collision.gameObject.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            SpawnUntilCount(countSpawnInfo);
        }

        #endregion Unity function

        #endregion private void
    }
}