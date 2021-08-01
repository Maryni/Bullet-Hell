using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global.UI
{
    public class SettingsAction : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private InputField[] inputFields;
        [SerializeField] private DataManager dataManager;
#pragma warning restore

        #endregion private variables

        #region public void

        public void GetValue(int index)
        {
            if (index > 0 && index < inputFields.Length + 1)
            {
                print(inputFields[index - 1].text);
                if (inputFields[index - 1].text != "")
                {
                    dataManager.AddData(inputFields[index - 1].text);
                }
            }
            else
                Debug.LogError("index is incorrent, use index+1 (0 -> 1| 1 -> 2)");
        }

        #endregion public void
    }
}