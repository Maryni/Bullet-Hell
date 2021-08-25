using UnityEngine;
using Global.Managers.Datas;

namespace Global.ActiveObjects
{
    public class MeleeEnemy : BaseEnemy
    {
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
        }

        private void Start()
        {
            SetTransformPlayer();
        }

        #endregion Unity functions

        #region public void

        public override void GetDamage(float damage)
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