using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global.UI
{
    public class SettingsLoader : MonoBehaviour
    {
        [SerializeField] private Dropdown startWeapon;
        [SerializeField] private Dropdown cameraType;
        [SerializeField] private InputField rocketTimerBlowUp;
        [SerializeField] private InputField rocketRadiusBlowUp;
        [SerializeField] private InputField timerToSpawnItem;
        [SerializeField] private InputField timerToDisableItems;
        [SerializeField] private InputField playerSpeed;

        private void Start()
        {
            var data = Services.GetManager<DataManager>().DynamicData;
            LoadSettings(rocketTimerBlowUp, data.RocketData.timeToBlowUp.ToString());
            LoadSettings(rocketRadiusBlowUp, data.RocketData.radiusBlowUp.ToString());
            LoadSettings(timerToSpawnItem, data.SpawnItemData.spawnTime.ToString());
            LoadSettings(timerToDisableItems, data.SpawnItemData.destroyTime.ToString());
            LoadSettings(playerSpeed, data.PlayerData.speed.ToString());
            LoadSettings(startWeapon, data.StartPlayerWeapon.ToString());
            LoadSettings(cameraType, data.StartCameraType.ToString());
        }

        private void LoadSettings(InputField input, string value)
        {
            input.text = value;
        }

        private void LoadSettings(Dropdown input, string value)
        {
            for (int i = 0; i < input.options.Count; i++)
            {
                if (input.options[i].text == value)
                {
                    input.value = i;
                }
            }
        }
    }
}