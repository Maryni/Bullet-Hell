using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Global.Camera
{
    public class CameraType : MonoBehaviour
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
            CameraTypeChange();
        }

        #endregion Unity functions

        #region private void

        private void CameraTypeChange()
        {
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