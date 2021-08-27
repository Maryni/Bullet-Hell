using UnityEngine;

namespace Global.Bullet
{
    public class AutomaticalBullet : BaseBullet
    {
        #region public void

        public override void Move()
        {
            Rig2D.AddForce(transform.up * BulletStats.speed, ForceMode2D.Impulse);
        }

        #endregion public void
    }
}