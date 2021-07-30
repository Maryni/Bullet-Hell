using System;
using UnityEngine;

namespace Global.Managers.Datas
{
    public class DataManager : BaseManager
    {
        private DynamicData dynamicData;

        public DynamicData DynamicData => dynamicData;
        public override Type ManagerType => typeof(DataManager);

        protected override bool OnInit()
        {
            return true;
        }
    }
}