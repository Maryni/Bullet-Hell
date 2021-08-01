using Global.Controllers.Spawn;
using Global.Managers.Datas;
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
        [SerializeField] private float timer;
        private float timerTemp;
        [SerializeField] private bool needToSpawn;
        [SerializeField] private ShootController shootController;
        [SerializeField] private ItemInfo currentItemInfo;
        [SerializeField] private int countSpawnInfo;
        [SerializeField] private DataManager dataManager;
#pragma warning restore

        #endregion private variables

        #region properties

        public Spawner Spawner => spawner;

        #endregion properties

        #region public void

        public void DisableSpawn() => needToSpawn = false;

        #endregion public void

        #region private void

        public void LoadSettings(float timer, float timerToSpawner)
        {
            this.timer = timer;
            spawner.SetTimer(timerToSpawner);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Item")
            {
                currentItemInfo = collision.gameObject.GetComponent<ItemInfo>();
                int value = int.Parse(currentItemInfo.GetValue());
                shootController.ChangeWeaponType(value);
                collision.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            timerTemp = timer;
        }

        private void FixedUpdate()
        {
            SpawnUntilCount(countSpawnInfo);
        }

        private void SpawnUntilCount(int count)
        {
            if (spawner.SpawnList.Count >= count)
            {
                needToSpawn = false;
            }
            if (timer > 0 && needToSpawn)
            {
                timer -= 0.1f;
            }
            if (timer <= 0 && needToSpawn)
            {
                spawner.Spawn();
                timer = timerTemp;
            }
        }

        #endregion private void
    }
}