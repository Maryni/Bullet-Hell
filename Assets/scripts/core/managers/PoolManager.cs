using System;
using UnityEngine;
using Global.Managers.Datas;

namespace Global.Managers
{
    public class PoolManager : BaseManager
    {
        [SerializeField] private BulletPool bulletPool;

        public BulletPool BulletPool => bulletPool;
        public override Type ManagerType => typeof(PoolManager);

        protected override bool OnInit()
        {
            return true;
        }
    }
}