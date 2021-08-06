using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.ActiveObjects
{
    public class EnemyController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private MeleeEnemy meleeEnemy;

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            meleeEnemy.Movement();
            meleeEnemy.Attack();
        }

        #endregion Unity functions
    }
}