using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    [CreateAssetMenu(fileName = "BulletStats", menuName = "Bullet/BulletStats")]
    public class BulletStats : ScriptableObject
    {
        public BulletType bulletType;
        public int damage;
        public float speed;
    }
}