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
        [SerializeField] private string currentText;
#pragma warning restore

        #endregion Inspector variables

        #region properties

        public string CurrentText => currentText;
        public VariableName VariableName => variableName;

        #endregion properties

        #region private variables

        private char[] numberArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private int valueInt;
        private float valueFloat;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            SetValueFromText();
        }

        #endregion Unity functions

        #region public void

        #region void AddLessValue

        public void AddValueToText(bool usedLikeFloat)
        {
            if (usedLikeFloat)
            {
                valueFloat += valueToFloat;
                currentText = valueFloat.ToString();
                textFieldUsed.text = currentText;
            }
            else
            {
                valueInt++;
                currentText = valueInt.ToString();
                textFieldUsed.text = currentText;
            }
        }

        public void LessValueFromText(bool usedLikeFloat)
        {
            if (usedLikeFloat)
            {
                valueFloat -= valueToFloat;
                currentText = valueFloat.ToString();
                textFieldUsed.text = currentText;
            }
            else
            {
                valueInt--;
                currentText = valueInt.ToString();
                textFieldUsed.text = currentText;
            }
        }

        #endregion void AddLessValue

        public void SetTextFromValue()
        {
            if (textUsed != null)
            {
                if (valueFloat != 0)
                {
                    currentText = valueFloat.ToString();
                    textUsed.text = currentText;
                }
                if (valueInt != 0)
                {
                    currentText = valueFloat.ToString();
                    textUsed.text = currentText;
                }
            }
            if (textFieldUsed != null)
            {
                if (valueFloat != 0)
                {
                    currentText = valueFloat.ToString();
                    textFieldUsed.text = currentText;
                }
                if (valueInt != 0)
                {
                    currentText = valueFloat.ToString();
                    textFieldUsed.text = currentText;
                }
            }
        }

        public void SetValueFromText()
        {
            if (textFieldUsed != null)
            {
                valueFloat = float.Parse(ValueCheckerOnOnlyNumbers(textFieldUsed.text));
                currentText = valueFloat.ToString();
            }
            else
            {
                currentText = textUsed.text;
            }
        }

        #endregion public void

        #region private void

        private string ValueCheckerOnOnlyNumbers(string text)
        {
            string textValue = text;
            float floatValue = 0;

            textValue = text.Replace('.', ',');
            if (!float.TryParse(textValue, out floatValue))
            {
                var textAfterParse = "";
                var charArray = textValue.ToCharArray();
                for (int i = 0; i < charArray.Length; i++)
                {
                    for (int j = 0; j < numberArray.Length; j++)
                    {
                        if (charArray[i] == numberArray[j] || charArray[i] == ',')
                        {
                            textAfterParse += charArray[i];
                            break;
                        }
                    }
                }
                floatValue = float.Parse(textAfterParse);
            }
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