using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Global.Managers.Datas;

namespace Global.Bullet
{
    public abstract class BaseBullet : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private Rigidbody2D rig2D;
        [SerializeField] private BulletData bulletStats;
#pragma warning restore

        #endregion Inspector variables

        #region properties

        public BulletData BulletStats => bulletStats;
        public Rigidbody2D Rig2D => rig2D;

        #endregion properties

        #region protected variables

        protected Coroutine coroutineOnCollisionCalled;

        #endregion protected variables

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

        public virtual void TriggetBulletOnCollision()
        {
            gameObject.SetActive(false);
            StopCoroutine(coroutineOnCollisionCalled);
            coroutineOnCollisionCalled = null;
        }

        public void Rotate(float angle)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        }

        public abstract void Move();

        public void ActivateBullet()
        {
            Init();
        }

        protected void Init()
        {
            if (BulletStats.bulletType == BulletType.RocketLaucherBullet)
            {
                bulletStats.damage = Services.GetManager<DataManager>().DynamicData.GetBulletDataByType(BulletType.ShotgunBullet).damage;
                bulletStats.speed = Services.GetManager<DataManager>().DynamicData.RocketData.minSpeed;
            }
            if (BulletStats.bulletType == BulletType.ShotgunBullet)
            {
                bulletStats = Services.GetManager<DataManager>().DynamicData.GetBulletDataByType(BulletType.ShotgunBullet);
            }
            if (BulletStats.bulletType == BulletType.AutomaticBullet)
            {
                bulletStats = Services.GetManager<DataManager>().DynamicData.GetBulletDataByType(BulletType.AutomaticBullet);
            }
        }

        #endregion public void
    }
}