using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Global.Camera
{
    //нужно сделать, чтобы этот скрипт висел на камере
    //не игрок должен говорить камере сделать за ним
    //а камера должна знать за кем следить
    public class CameraFollow : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private GameObject camera;
        [SerializeField] private GameObject player;
        [Header("0 - static, 1 - following"), SerializeField] private int typeFollowingCamera; //тип должен быть задан не число, а перечеслением
        private UnityEvent cameraFollow; //неправильное форматирование и это было указано еще в первом код ревью
        private float smoothSpeed = 0.125f; //неправильное форматирование и это было указано еще в первом код ревью
        [SerializeField] private Vector3 offset;
#pragma warning restore

        #endregion private variables

        #region public void

        public void InvokeFollowing()
        {
            cameraFollow.Invoke();
        }

        public void CameraFollowing()
        {
            if (typeFollowingCamera == 0)
            {
                camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            }
            if (typeFollowingCamera == 1)
            {
                Vector3 desiredPosition = player.transform.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(camera.transform.position, desiredPosition, smoothSpeed);
                camera.transform.position = smoothedPosition;
                camera.transform.LookAt(player.transform);
            }
        }

        #endregion public void

        #region private void

        #region Unity function

        private void Start()
        {
            if (cameraFollow == null)
                cameraFollow = new UnityEvent();
            cameraFollow.AddListener(CameraFollowing);
        }

        #endregion Unity function

        #endregion private void
    }
}