using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Interfaces.Player;

namespace Global.Player
{
    public class Player : MonoBehaviour, IPlayerStats
    {
        #region private variables

#pragma warning disable
        [SerializeField] private int hp = 100;
        [SerializeField] private float speed;
#pragma warning restore

        #endregion private variables

        #region properties

        public int HP => hp;
        public float Speed => speed;

        #endregion properties

        #region public void

        public void GetDamage(int damage)
        {
            hp -= damage;
        }

        #endregion public void
    }
}