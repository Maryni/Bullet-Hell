using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global.Save;

namespace Global.UI
{
    public class SettingsItem : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private VariableName variableName;
        [SerializeField] private Text textUsed;
        [SerializeField] private InputField textFieldUsed;
        [SerializeField] private float valueToFloat;
        [SerializeField] private List<InputField> listFields;

#pragma warning restore

        #endregion Inspector variables

        #region properties

        public VariableName VariableName => variableName;

        #endregion properties

        #region private variables

        private int valueInt;
        private float valueFloat;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            LoadValueFromData();
            SetValueFromText();
        }

        #endregion Unity functions

        #region public void

        public void SetStartWeaponByText()
        {
            WeaponType weaponType = WeaponType.AutomaticGun;
            if (textUsed.text == WeaponType.AutomaticGun.ToString())
            {
                weaponType = WeaponType.AutomaticGun;
            }
            if (textUsed.text == WeaponType.Shotgun.ToString())
            {
                weaponType = WeaponType.Shotgun;
            }
            if (textUsed.text == WeaponType.RocketLaucher.ToString())
            {
                weaponType = WeaponType.RocketLaucher;
            }

            Services.GetManager<DataManager>().DynamicData.SetStartPlayerWeapon(weaponType);
        }

        public void AddValueToText(bool textFieldUsedBool)
        {
            if (textFieldUsedBool)
            {
                valueInt++;
                textFieldUsed.text = valueInt.ToString();
            }
            if (!textFieldUsedBool)
            {
                valueInt++;
                textUsed.text = valueInt.ToString();
            }
        }

        public void LessValueFromText(bool textFieldUsedBool)
        {
            if (textFieldUsedBool)
            {
                valueInt--;
                textFieldUsed.text = valueInt.ToString();
            }
            if (!textFieldUsedBool)
            {
                valueInt--;
                textUsed.text = valueInt.ToString();
            }
        }

        public void AddValueToTextByFloat(bool textFieldUsedBool)
        {
            if (textFieldUsedBool)
            {
                valueFloat += valueToFloat;
                textFieldUsed.text = valueFloat.ToString();
            }
            if (!textFieldUsedBool)
            {
                valueFloat += valueToFloat;
                textUsed.text = valueFloat.ToString();
            }
        }

        public void LessValueFromTextByFloat(bool textFieldUsedBool)
        {
            if (textFieldUsedBool)
            {
                valueFloat -= valueToFloat;
                textFieldUsed.text = valueFloat.ToString();
            }
            if (!textFieldUsedBool)
            {
                valueFloat -= valueToFloat;
                textUsed.text = valueFloat.ToString();
            }
        }

        public void SaveDataOnExit()
        {
            if (listFields.Count > 0)
            {
                var dataManager = Services.GetManager<DataManager>();
                for (int i = 0; i < listFields.Count; i++)
                {
                    dataManager.DynamicData.SetValueToData(listFields[i].transform.parent.GetComponent<SettingsItem>().VariableName,
                        listFields[i].transform.parent.GetComponent<SettingsItem>().textFieldUsed.text);
                }

                dataManager.SaveDynamicData();
            }
        }

        #endregion public void

        #region private void

        private void SetValueFromText()
        {
            if (variableName == VariableName.RocketDataTimeToBlowUp)
            {
                valueInt = int.Parse(textFieldUsed.text);
            }
            if (variableName == VariableName.RocketDataRadiusToBlowUp)
            {
                valueFloat = float.Parse(textFieldUsed.text);
            }
        }

        private void LoadValueFromData()
        {
            if (variableName == VariableName.RocketDataTimeToBlowUp)
            {
                var dataManager = Services.GetManager<DataManager>();
                if (textFieldUsed != null)
                {
                    textFieldUsed.text = dataManager.DynamicData.RocketData.timeToBlowUp.ToString();
                }
                if (textUsed != null)
                {
                    textUsed.text = dataManager.DynamicData.RocketData.timeToBlowUp.ToString();
                }
            }
            if (variableName == VariableName.RocketDataRadiusToBlowUp)
            {
                var dataManager = Services.GetManager<DataManager>();
                if (textFieldUsed != null)
                {
                    textFieldUsed.text = dataManager.DynamicData.RocketData.radiusBlowUp.ToString();
                }
                if (textUsed != null)
                {
                    textUsed.text = dataManager.DynamicData.RocketData.radiusBlowUp.ToString();
                }
            }
        }
    }

    #endregion private void
}