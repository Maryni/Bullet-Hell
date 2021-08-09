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
        [SerializeField] private int countEnemyOnScreen;

#pragma warning restore

        #endregion Inspector variables

        private void Start()
        {
            Services.GetManager<SpawnManager>().SpawnEnemy(EnemyType.MeleeGrounded_MiddleSpeed);
        }
    }
}