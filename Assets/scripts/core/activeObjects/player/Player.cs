using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Interfaces;

namespace Global.Player
{
    public class Player : MonoBehaviour, IActiveOnSceneObjectStats
    {
        #region private variables

#pragma warning disable
        [SerializeField] private int hp = 100;
        [SerializeField] private float speed;
        [SerializeField] private int hpValue;
#pragma warning restore

        #endregion private variables

        #region properties

        public int HP => hp;
        public float Speed => speed;
        public int HPValue => hpValue;

        #endregion properties

        #region public void

        public void GetDamage(int damage)
        {
            hpValue -= damage;
        }

        public bool IsDead()
        {
            if (hpValue < 0)
            {
                return true;
            }
            return false;
        }

        #endregion public void

        #region private void

        #region Unity functions

        private void Start()
        {
            RestoreHPToMaxHP();
        }

        #endregion Unity functions

        private void RestoreHPToMaxHP()
        {
            hpValue = hp;
        }

        #endregion private void
    }
}