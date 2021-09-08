using Global.Managers.Datas;
using Global.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Controllers
{
    public class SettingsController : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private List<SettingsItem> settingsItems;
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
        }

        #endregion Unity functions

        #region public variables

        public void SaveDataOnExit()
        {
            if (settingsItems.Count > 0)
            {
                var dataManager = Services.GetManager<DataManager>();
                for (int i = 0; i < settingsItems.Count; i++)
                {
                    dataManager.
                        DynamicData.
                        SetValueToData(
                        settingsItems[i].VariableName,
                        settingsItems[i].CurrentText);
                }
                dataManager.SaveDynamicData();
            }
        }

        #endregion public variables
    }
}