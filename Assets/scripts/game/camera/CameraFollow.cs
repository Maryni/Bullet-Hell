using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Global.Camera
{
    public class CameraFollow : MonoBehaviour
    {
#pragma warning disable
        [SerializeField] private GameObject camera;
        [SerializeField] private GameObject player;
        [Header("0 - static, 1 - following"), SerializeField] private int typeFollowingCamera;
        private UnityEvent cameraFollow;
        private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset;
#pragma warning restore

        private void Start()
        {
            if (cameraFollow == null)
                cameraFollow = new UnityEvent();
            cameraFollow.AddListener(CameraFollowing);
        }

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
    }
}