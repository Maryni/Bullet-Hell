using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    [Serializable]
    public class DynamicData
    {
        #region private variables

        private List<string> arrayData = new List<string>();

        #endregion private variables

        #region properties

        public List<string> ArrayData => arrayData;

        #endregion properties

        #region public void

        public void AddData(string value)
        {
            arrayData.Add(value);
        }

        public string GetDataByIndex(int index) => arrayData[index];

        public string GetDataAllLines()
        {
            string temp = "";
            foreach (string item in arrayData)
            {
                temp += (item + "|");
            }
            return temp;
        }

        public void SetDataByLine(string value)
        {
            string[] temp = value.Split('|');
            foreach (string item in temp)
            {
                arrayData.Add(item);
            }
        }

        #endregion public void
    }
}