using UnityEngine;

namespace Global.Bullet
{
    public class ShotgunBullet : BaseBullet
    {
        public void Rotate(Vector3 rotate)
        {
        }

        public override void Move(Vector2 pos, Transform pointForShooting)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, BulletStatsShotgun.angel, transform.rotation.z);
            Vector2 direction = pointForShooting.up;
            Rig2D.AddForce(direction * BulletStats.speed, ForceMode2D.Impulse);
        }
    }
}