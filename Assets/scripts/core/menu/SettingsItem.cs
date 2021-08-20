using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global.Save;
using Global.Camera;

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
        private char[] arrayNumbers = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            LoadValueFromData();
            SetValueFromText();
        }

        #endregion Unity functions

        #region public void

        public void SetCameraTypeByText()
        {
            GameCameraType cameraType = GameCameraType.DynamicCamera;
            if (textUsed.text == GameCameraType.StaticCamera.ToString())
            {
                cameraType = GameCameraType.StaticCamera;
            }
            Services.GetManager<DataManager>().DynamicData.SetStartCameraType(cameraType);
        }

        public void SetStartWeaponByText()
        {
            WeaponType weaponType = WeaponType.AutomaticGun;
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

        #region void AddLessValue

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

        #endregion void AddLessValue

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

        public void SetTextFromValue()
        {
            if (textUsed != null)
            {
                if (valueFloat != 0)
                {
                    textUsed.text = valueFloat.ToString();
                }
                if (valueInt != 0)
                {
                    textUsed.text = valueInt.ToString();
                }
            }
            if (textFieldUsed != null)
            {
                if (valueFloat != 0)
                {
                    textFieldUsed.text = valueFloat.ToString();
                }
                if (valueInt != 0)
                {
                    textFieldUsed.text = valueInt.ToString();
                }
            }
        }

        #endregion public void

        #region private void

        private void SetValueFromText()
        {
            if (variableName == VariableName.RocketDataTimeToBlowUp)
            {
                valueInt = int.Parse(ValueCheckerOnOnlyNumbers(textFieldUsed.text));
            }
            if (variableName == VariableName.RocketDataRadiusToBlowUp)
            {
                valueFloat = float.Parse(ValueCheckerOnOnlyNumbers(textFieldUsed.text));
            }
            if (variableName == VariableName.SpawnItemDataTimeToSpawn)
            {
                valueInt = int.Parse(ValueCheckerOnOnlyNumbers(textFieldUsed.text));
            }
            if (variableName == VariableName.SpawnItemDataTimeToHideWeaponAfterSpawn)
            {
                valueInt = int.Parse(ValueCheckerOnOnlyNumbers(textFieldUsed.text));
            }
            if (variableName == VariableName.PlayerSpeed)
            {
                valueInt = int.Parse(ValueCheckerOnOnlyNumbers(textFieldUsed.text));
            }
        }

        private string ValueCheckerOnOnlyNumbers(string text)
        {
            string textValue = "";
            var textChar = text.ToCharArray();

            for (int i = 0; i < textChar.Length; i++)
            {
                for (int j = 0; j < arrayNumbers.Length; j++)
                {
                    if (textChar[i] == arrayNumbers[j] || textChar[i] == '.' || textChar[i] == ',')
                    {
                        textValue += textChar[i];
                        break;
                    }
                }
            }
            if (textValue == "")
            {
                textValue = "0";
            }

            return textValue;
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
            if (variableName == VariableName.SpawnItemDataTimeToSpawn)
            {
                var dataManager = Services.GetManager<DataManager>();
                if (textFieldUsed != null)
                {
                    textFieldUsed.text = dataManager.DynamicData.SpawnItemData.spawnTime.ToString();
                }
                if (textUsed != null)
                {
                    textUsed.text = dataManager.DynamicData.SpawnItemData.spawnTime.ToString();
                }
            }
            if (variableName == VariableName.SpawnItemDataTimeToHideWeaponAfterSpawn)
            {
                var dataManager = Services.GetManager<DataManager>();
                if (textFieldUsed != null)
                {
                    textFieldUsed.text = dataManager.DynamicData.SpawnItemData.destroyTime.ToString();
                }
                if (textUsed != null)
                {
                    textUsed.text = dataManager.DynamicData.SpawnItemData.destroyTime.ToString();
                }
            }
            if (variableName == VariableName.PlayerSpeed)
            {
                var dataManager = Services.GetManager<DataManager>();
                if (textFieldUsed != null)
                {
                    textFieldUsed.text = dataManager.DynamicData.PlayerData.speed.ToString();
                }
                if (textUsed != null)
                {
                    textUsed.text = dataManager.DynamicData.PlayerData.speed.ToString();
                }
            }
            if (variableName == VariableName.StartWeaponType)
            {
                if (textUsed != null)
                {
                    var dataManager = Services.GetManager<DataManager>();
                    var dropDown = textUsed.gameObject.GetComponentInParent<Dropdown>();
                    for (int i = 0; i < dropDown.options.Count; i++)
                    {
                        if (dropDown.options[i].text == dataManager.DynamicData.StartPlayerWeapon.ToString())
                        {
                            dropDown.value = i;
                        }
                    }
                }
                else
                {
                    Debug.LogError("textUsed for this VariableName not have reference");
                }
            }
            if (variableName == VariableName.StartCameraType)
            {
                if (textUsed != null)
                {
                    var dataManager = Services.GetManager<DataManager>();
                    var dropDown = textUsed.gameObject.GetComponentInParent<Dropdown>();
                    for (int i = 0; i < dropDown.options.Count; i++)
                    {
                        if (dropDown.options[i].text == dataManager.DynamicData.StartCameraType.ToString())
                        {
                            dropDown.value = i;
                        }
                    }
                }
                else
                {
                    Debug.LogError("textUsed for this VariableName not have reference");
                }
            }
        }

        #endregion private void
    }
}