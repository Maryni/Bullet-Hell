﻿using Global.Interfaces.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Shooting.BulletSpace
{
    public class Bullet : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float modSpeedBullet;
        [SerializeField] private Vector3 backupTransformVector3;//
        [SerializeField] private int typeBullet;//
        [SerializeField] private Transform parent;//
        [SerializeField] private float timerToBlowUp;
        private bool isMoving = false;//
        private float timerTemp;//
        [SerializeField] private float modBlowUp = 1f;//
        [SerializeField] private WeaponSettings.typeBullet typeBulletEnum;//

#pragma warning restore

        #endregion private variables

        #region properties

        public int TypeBullet => typeBullet;

        #endregion properties

        #region public void

        public void GetTypeBullet(WeaponSettings settings)
        {
            switch (typeBullet)
            {
                case 0: typeBulletEnum = settings.TypeBulletAutomatic; break;
                case 1: typeBulletEnum = settings.TypeBulletShotgun; break;
                case 2: typeBulletEnum = settings.TypeBulletRocket; break;
            }
        }

        public void SetTimer(float timer)
        {
            timerToBlowUp = timer;
            timerTemp = timerToBlowUp;
        }

        public void Move(Vector2 pos)
        {
            Vector2 direction = pos - (Vector2)transform.position;
            rigidbody2D.AddForce(direction * modSpeedBullet, ForceMode2D.Impulse);
            isMoving = true;
        }

        public void Move(Vector2 pos, Vector3 angle)
        {
            Vector2 direction = pos - (Vector2)transform.position;
            direction = direction * angle;
            rigidbody2D.AddForce(direction * modSpeedBullet, ForceMode2D.Impulse);
        }

        public void GetBackToParent()
        {
            SetBackupVectorPosition();
            gameObject.transform.position = backupTransformVector3;
            this.gameObject.SetActive(false);
        }

        #endregion public void

        #region private void

        #region Unity function

        private void Start()
        {
            backupTransformVector3 = this.gameObject.transform.localPosition;
            timerTemp = timerToBlowUp;
        }

        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy)
            {
                if (typeBulletEnum == WeaponSettings.typeBullet.RocketLaucher && isMoving)
                {
                    Explosion();
                }
            }
        }

        #endregion Unity function

        private void SetBackupVectorPosition()
        {
            backupTransformVector3 = new Vector3(parent.localPosition.x, parent.localPosition.y, 0);
        }

        private void Explosion()
        {
            if (timerToBlowUp > 0)
            {
                timerToBlowUp -= 0.1f;
            }
            if (timerToBlowUp < 0)
            {
                float temp = gameObject.GetComponent<CircleCollider2D>().radius;
                gameObject.GetComponent<CircleCollider2D>().radius = temp * modBlowUp;
                GetBackToParent();
                isMoving = false;
                gameObject.GetComponent<CircleCollider2D>().radius = temp;
                timerToBlowUp = timerTemp;
            }
        }

        #endregion private void
    }
}