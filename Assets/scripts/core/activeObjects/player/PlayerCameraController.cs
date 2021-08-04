using Global.Camera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private CameraFollow cameraFollow;
#pragma warning restore

        #endregion private variables

        #region private void

        #region Unity function

        private void LateUpdate()
        {
            cameraFollow.InvokeFollowing();
        }

        #endregion Unity function

        #endregion private void
    }
}