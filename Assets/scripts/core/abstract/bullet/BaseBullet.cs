using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Global.Managers.Datas;

namespace Global.Managers.Datas
{
    [Serializable]
    public class BulletStats
    {
        //TODO: move BulletStats to ScriptableObjects, add bulletStats to nessasaryWeapon
        public int damage;

        public int speed;
    }
}

namespace Global.Bullet
{
    public abstract class BaseBullet : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private Rigidbody2D rig2D;
        [SerializeField] private BulletStats bulletStats;
#pragma warning restore

        #endregion Inspector variables

        #region properties

        public BulletStats BulletStats => bulletStats;
        public Rigidbody2D Rig2D => rig2D;

        #endregion properties

        #region Unity functions

        private void OnValidate()
        {
            if (rig2D == null)
            {
                rig2D = GetComponent<Rigidbody2D>();
            }
        }

        #endregion Unity functions

        #region public void

        /// <summary>
        ///
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pointForShooting">cannon (like tank big gun)</param>
        public abstract void Move();

        #endregion public void
    }
}