using Global.Controllers.Spawn;
using Global.Managers;
using Global.Managers.Datas;
using Global.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Controllers
{
    public class SpawnController : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable

        [SerializeField] private float timeForEachSpawn;
        [SerializeField] private bool coroutineEnable = true;

#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            //Services.GetManager<SpawnManager>().SpawnEnemy(EnemyType.MeleeGrounded_MiddleSpeed);
            StartCoroutine(SpawnEnemiesPerTime(timeForEachSpawn));
        }

        #endregion Unity functions

        #region public void

        public IEnumerator SpawnEnemiesPerTime(float time)
        {
            if (coroutineEnable)
            {
                var type = Services.GetManager<SpawnManager>().GetRandomEnemyType();

                Services.GetManager<SpawnManager>().SpawnEnemy(type);
                yield return new WaitForSeconds(time);
            }
            SpawnEnemiesPerTime(time);
        }

        #endregion public void
    }
}