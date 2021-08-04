using Global.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Bullet
{
    public class AutomaticalBullet : BaseBullet
    {
        public override void Move(Vector2 pos, Transform pointForShooting)
        {
            Vector2 direction = pointForShooting.up;
            Rig2D.AddForce(direction * BulletStats.speed, ForceMode2D.Impulse);
        }
    }
}