using UnityEngine;
using Global.Managers.Datas;

namespace Global.ActiveObjects
{
    public class MeleeEnemy : BaseEnemy
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private float rateMovement;

#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Awake()
        {
            Init(enemyType);
        }

        private void OnValidate()
        {
            if (rig2d == null)
            {
                rig2d = GetComponent<Rigidbody2D>();
            }
            if (enemyMovement == null)
            {
                enemyMovement = GetComponent<EnemyMovement>();
            }
        }

        private void Start()
        {
            SetTransformPlayer();
            ResetEnemyHP();
        }

        #endregion Unity functions

        #region public void

        public override void ObjectTriggered(int damage)
        {
            EnemyStats.hpValueCurrent -= DamageTakenCalculator(damage);
            if (EnemyStats.hpValueCurrent <= 0)
            {
                Dead();
            }
        }

        public override void ObjectTriggered(float damage)
        {
            EnemyStats.hpValueCurrent -= DamageTakenCalculator(damage);
            if (EnemyStats.hpValueCurrent <= 0)
            {
                Dead();
            }
        }

        #endregion public void
    }
}