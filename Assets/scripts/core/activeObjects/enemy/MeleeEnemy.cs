using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Player;

namespace Global.ActiveObjects
{
    public class MeleeEnemy : BaseEnemy
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private Transform transformPlayer;
        [SerializeField] private Rigidbody2D rig2d;
        [SerializeField] private float timeInvokeReapeating;
        [SerializeField] private float rateInvokeReapeatingMovement;
        [SerializeField] private float rateInvokeReapeatingReset;
        [SerializeField] private EnemyMovement enemyMovement;
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            Init();
            InvokeRepeating("ResetVelocity", timeInvokeReapeating, rateInvokeReapeatingReset);
            InvokeRepeating("Movement", timeInvokeReapeating, rateInvokeReapeatingMovement);
        }

        private void OnValidate()
        {
            rig2d = GetComponent<Rigidbody2D>();
        }

        #endregion Unity functions

        #region public void

        public override void Movement()
        {
            SetPlayerTransform();
            transform.rotation = Rotation(transformPlayer.position, transform);
            enemyMovement.Movement(transformPlayer, rig2d, EnemyStats.speed);
        }

        public override void Attack()
        {
            Debug.Log("Attacking");
        }

        #endregion public void

        #region private void

        private void EnableTriggerChecker() => transform.GetComponentInChildren<PlayerTriggerChecker>().enabled = true;

        private void ResetVelocity() => rig2d.velocity = Vector2.zero;

        private void SetPlayerTransform() => transformPlayer = FindObjectOfType<Player.Player>().transform;

        private Quaternion Rotation(Vector3 mousePos, Transform transformObject)
        {
            float AngleRad = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x);
            AngleRad = (180 / Mathf.PI) * AngleRad;
            AngleRad += 90;
            AngleRad += 180;
            return transformObject.rotation = Quaternion.Euler(0, 0, AngleRad);
        }

        #endregion private void
    }
}