using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global.UI
{
    public class SettingsItem : MonoBehaviour
    {
        //in future it will save

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

        #endregion public void
    }
}