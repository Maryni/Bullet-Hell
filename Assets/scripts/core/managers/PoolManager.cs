using System;
using UnityEngine;
using Global.Managers.Datas;

namespace Global.Managers
{
    public class PoolManager : BaseManager
    {
        [SerializeField] private BulletPool bulletPool;

        [SerializeField] private EnemyPool enemyPool;
        [SerializeField] private WeaponPool weaponPool;

        public BulletPool BulletPool => bulletPool;
        public EnemyPool EnemyPool => enemyPool;
        public WeaponPool WeaponPool => weaponPool;

        public override Type ManagerType => typeof(PoolManager);

        protected override bool OnInit()
        {
            return true;
        }
    }
}