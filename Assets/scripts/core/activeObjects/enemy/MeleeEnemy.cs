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
        [SerializeField] private EnemyMovement enemyMovement; //зачем ты сериализируешь поле, если ты его в старте ищешь?
        //+ что я говорил ранее за онвалидейт?
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            Init();
            enemyMovement = GetComponent<EnemyMovement>();
        }

        private void OnValidate() //тут он у тебя есть, но мувмент ты ищешь в старте
        {
            rig2d = GetComponent<Rigidbody2D>();
        }

        #endregion Unity functions

        #region public void

        public override void Movement()
        {
            InvokeRepeating("ResetVelocity", timeInvokeReapeating, rateInvokeReapeatingReset); //избавляйся от инвоук репитинг
            InvokeRepeating("Move", timeInvokeReapeating, rateInvokeReapeatingMovement); //избавляйся от инвоук репитинг
        }

        public override void ObjectTriggered()
        {
            base.ObjectTriggered();
            Debug.Log($"Im Enemy[ {name} ] triggered");
            transform.parent.gameObject.SetActive(false);
        }

        #endregion public void

        #region private void

        private void ResetVelocity() => rig2d.velocity = Vector2.zero; //неправильное форматирование, ставь скобки, это функция. Сокращение в данном случае - неуместно

        private void SetPlayerTransform() => transformPlayer = FindObjectOfType<Player.Player>().transform; //неправильное форматирование, ставь скобки, это функция. Сокращение в данном случае - неуместно

        private Quaternion Rotation(Vector3 mousePos, Transform transformObject)
        {
            float AngleRad = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x);
            AngleRad = (180 / Mathf.PI) * AngleRad;
            AngleRad += 90;
            AngleRad += 180;
            return transformObject.rotation = Quaternion.Euler(0, 0, AngleRad);
        }

        private void Move()
        {
            SetPlayerTransform(); //ты каждое Н время получаешь игрока через FindObjectOfType. УДОЛИ!
            transform.rotation = Rotation(transformPlayer.position, transform);
            enemyMovement.Movement(transformPlayer, rig2d, EnemyStats.speed);
        }

        #endregion private void
    }
}