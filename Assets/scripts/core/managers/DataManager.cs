using System;
using UnityEngine;

namespace Global.Managers.Datas
{
    public class DataManager : BaseManager
    {
        #region private variables

        private DynamicData dynamicData;

        #endregion private variables

        #region properties

        public DynamicData DynamicData => dynamicData;
        public override Type ManagerType => typeof(DataManager);

        #endregion properties

        protected override bool OnInit()
        {
            return true;
        }

        #region public void

        public void AddData(string value)
        {
            if (dynamicData.ArrayData.Count >= 0 && dynamicData.ArrayData.Count < 5)
            {
                dynamicData.AddData(value);
            }
            SaveData();
            print(dynamicData.GetDataAllLines());
        }

        public void GetData(int index)
        {
            dynamicData.GetDataByIndex(index);
        }

        public void SaveData()
        {
            PlayerPrefs.SetString("data", dynamicData.GetDataAllLines());
        }

        #endregion public void

        #region private void

        private void LoadData()
        {
            if (PlayerPrefs.GetString("data").Length > 0)
            {
                dynamicData.SetDataByLine(PlayerPrefs.GetString("data"));
            }
        }

        private void Awake()
        {
            dynamicData = new DynamicData();
            LoadData();
        }

        #endregion private void
    }
}