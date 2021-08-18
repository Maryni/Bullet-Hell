using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Global.Managers.Datas;

namespace Global.Camera
{
    public enum GameCameraType
    {
        StaticCamera,
        DynamicCamera
    }

    public class CameraTypeChange : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private GameObject cameraObject;
        [SerializeField] private bool isStaticCamera;
        [SerializeField] private float[,] listValue = new float[2, 2] { { 0.4f, 0.5f }, { 0.2f, 0.25f } };
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            CameraTypeChanging();
        }

        #endregion Unity functions

        #region private void

        private void CameraTypeChanging()
        {
            var cameraType = Services.GetManager<DataManager>().DynamicData.StartCameraType;
            if (cameraType == GameCameraType.DynamicCamera)
            {
                isStaticCamera = false;
            }
            if (cameraType == GameCameraType.StaticCamera)
            {
                isStaticCamera = true;
            }
            if (!isStaticCamera)
            {
                var composer = cameraObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
                composer.m_DeadZoneHeight = listValue[0, 0];
                composer.m_DeadZoneWidth = listValue[0, 1];
            }
            else
            {
                var composer = cameraObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
                composer.m_DeadZoneHeight = listValue[1, 0];
                composer.m_DeadZoneWidth = listValue[1, 1];
            }
        }

        #endregion private void
    }
}