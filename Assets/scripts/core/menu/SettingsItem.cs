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
        [SerializeField] private List<Text> listTexts;
#pragma warning restore

        #endregion Inspector variables

        #region public void

        public void SetStartWeaponByText(Text textWeapon)
        {
            WeaponType weaponType = WeaponType.AutomaticGun;
            if (textWeapon.text == WeaponType.AutomaticGun.ToString())
            {
                weaponType = WeaponType.AutomaticGun;
            }
            if (textWeapon.text == WeaponType.Shotgun.ToString())
            {
                weaponType = WeaponType.Shotgun;
            }
            if (textWeapon.text == WeaponType.RocketLaucher.ToString())
            {
                weaponType = WeaponType.RocketLaucher;
            }

            Services.GetManager<DataManager>().DynamicData.SetStartPlayerWeapon(weaponType);
        }

        public void AddValueToText(Text textObjectForAdd)
        {
            var parseValue = int.Parse(textObjectForAdd.text);
            parseValue++;
            textObjectForAdd.text = parseValue.ToString();
        }

        public void LessValueFromText(Text textObjectForAdd)
        {
            var parseValue = int.Parse(textObjectForAdd.text);
            parseValue--;
            textObjectForAdd.text = parseValue.ToString();
        }

        public void AddValueToTextByFloat(Text textObjectForAdd)
        {
            var parseValue = float.Parse(textObjectForAdd.text);
            parseValue += 0.1f;
            textObjectForAdd.text = parseValue.ToString();
        }

        public void LessValueFromTextByFloat(Text textObjectForAdd)
        {
            var parseValue = float.Parse(textObjectForAdd.text);
            parseValue -= 0.1f;
            textObjectForAdd.text = parseValue.ToString();
        }

        public void SaveDataOnExit()
        {
            var dataManager = Services.GetManager<DataManager>();
            dataManager.DynamicData.RocketData.timeToBlowUp = int.Parse(listTexts[0].text);
            dataManager.DynamicData.RocketData.radiutBlowUp = float.Parse(listTexts[1].text);
            dataManager.SaveDynamicData();
        }

        #endregion public void
    }
}