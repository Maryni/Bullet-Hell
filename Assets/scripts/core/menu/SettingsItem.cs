using Global.Managers.Datas;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        public void AddValueToText(bool usedLikeFloat)
        {
            if (usedLikeFloat)
            {
                valueFloat += valueToFloat;
                textFieldUsed.text = valueFloat.ToString();
            }
            else
            {
                valueInt++;
                textFieldUsed.text = valueInt.ToString();
            }
        }

        public void LessValueFromText(bool usedLikeFloat)
        {
            if (usedLikeFloat)
            {
                valueFloat -= valueToFloat;
                textFieldUsed.text = valueFloat.ToString();
            }
            else
            {
                valueInt--;
                textFieldUsed.text = valueInt.ToString();
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
            if (textFieldUsed != null)
            {
                valueFloat = float.Parse(ValueCheckerOnOnlyNumbers(textFieldUsed.text));
            }
        }

        private string ValueCheckerOnOnlyNumbers(string text)
        {
            string textValue = text;
            float floatValue = 0;

            Debug.Log("textValue = " + textValue);
            textValue = text.Replace('.', ',');
            float.TryParse(textValue, out floatValue);

            Debug.Log("floatValue = " + floatValue);
            textValue = floatValue.ToString();
            if (textValue == "")
            {
                textValue = "0";
            }

            return textValue;
        }

        #endregion private void
    }
}