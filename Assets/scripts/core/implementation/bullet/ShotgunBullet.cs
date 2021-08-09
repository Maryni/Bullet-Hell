using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Bullet
{
    public class ShotgunBullet : BaseBullet
    {
        #region public void

        public void Rotate(float angle, Transform transformCannon)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transformCannon.rotation.eulerAngles.z + angle);
        }

        public override void Move()
        {
            Rig2D.AddForce(transform.up * BulletStats.speed, ForceMode2D.Impulse);
        }

        #endregion public void
    }
}