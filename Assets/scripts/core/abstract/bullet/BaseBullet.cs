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

    [Serializable]
    public class BulletStatsShotgun : BulletStats
    {
        //TODO: move BulletStats to ScriptableObjects, add bulletStats to nessasaryWeapon
        public float angel;

        public float defaultAngel;
    }
}

namespace Global.Bullet
{
    public abstract class BaseBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rig2D;
        [SerializeField] private BulletStats bulletStats;
        [SerializeField] private BulletStatsShotgun bulletStatsShotgun;

        public BulletStats BulletStats => bulletStats;
        public BulletStatsShotgun BulletStatsShotgun => bulletStatsShotgun;
        public Rigidbody2D Rig2D => rig2D;

        public virtual void Init(BulletStats bulletStats)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pointForShooting">palka (like tank big gun)</param>
        public abstract void Move(Vector2 pos, Transform pointForShooting);

        private void OnValidate()
        {
            if (rig2D == null)
            {
                rig2D = GetComponent<Rigidbody2D>();
            }
        }
    }
}