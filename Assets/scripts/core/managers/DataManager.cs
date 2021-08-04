using System;
using UnityEngine;

namespace Global.Managers.Datas
{
    public class DataManager : BaseManager
    {
        #region private variables

#pragma warning disable
        [SerializeField] private DynamicData dynamicData;
        [SerializeField] private StaticData staticData;
#pragma warning restore

        #endregion private variables

        #region properties

        public DynamicData DynamicData => dynamicData;
        public StaticData StaticData => staticData;
        public override Type ManagerType => typeof(DataManager);

        #endregion properties

        protected override bool OnInit()
        {
            return true;
        }
    }
}