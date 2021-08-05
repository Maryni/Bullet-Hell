using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Bullet
{
    public class ShotgunBullet : BaseBullet
    {
        #region public void

        public void Rotate(float angle)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        }

        public override void Move(Transform pointForShooting)
        {
            Vector2 direction = pointForShooting.up;
            Rig2D.AddForce(direction * BulletStats.speed, ForceMode2D.Impulse);
        }

        #endregion public void
    }
}