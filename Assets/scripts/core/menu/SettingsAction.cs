using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global.UI
{
    public class SettingsAction : MonoBehaviour
    {
        //in future it will save

        #region public void

        public void AddValue(Text textObjectForAdd)
        {
            var parseValue = int.Parse(textObjectForAdd.text);
            parseValue++;
            textObjectForAdd.text = parseValue.ToString();
        }

        public void LessValue(Text textObjectForAdd)
        {
            var parseValue = int.Parse(textObjectForAdd.text);
            parseValue--;
            textObjectForAdd.text = parseValue.ToString();
        }

        #endregion public void
    }
}