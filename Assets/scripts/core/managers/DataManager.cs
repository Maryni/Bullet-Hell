using System;
using UnityEngine;
using Global.Save;

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

        public void SaveDynamicData()
        {
            SaveData.Save(dynamicData);
        }

        protected override bool OnInit()
        {
            SaveData.DefaultSave(dynamicData);
            dynamicData = SaveData.Load();
            dynamicData.SetActionsToDictionary();
            return true;
        }
    }
}