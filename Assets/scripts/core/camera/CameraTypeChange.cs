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
        [SerializeField] private float deadZoneHeight;
        [SerializeField] private float deadZoneWidth;
        [SerializeField] private GameObject cameraObjectStatic;
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
                cameraObjectStatic.SetActive(false);
                cameraObject.SetActive(true);
                var composer = cameraObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
                composer.m_DeadZoneHeight = deadZoneHeight;
                composer.m_DeadZoneWidth = deadZoneWidth;
            }
            if (isStaticCamera)
            {
                cameraObject.SetActive(false);
                cameraObjectStatic.SetActive(true);
            }
        }

        #endregion private void
    }
}