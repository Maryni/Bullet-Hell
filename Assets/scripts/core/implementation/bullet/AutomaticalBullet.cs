using Global.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Bullet
{
    public class AutomaticalBullet : BaseBullet
    {
        #region public void

        public void Rotate(Transform transformCannon)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transformCannon.rotation.eulerAngles.z);
        }

        public override void Move()
        {
            Rig2D.AddForce(transform.up * BulletStats.speed, ForceMode2D.Impulse);
        }

        #endregion public void
    }
}