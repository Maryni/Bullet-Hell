using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        public EnemyType enemyType;
        public int hpMaximum;
        public float speed;
        public float hpValueCurrent;
        public float damage;
        public int defence;
        public int intelligence;
        public float attackRate;

        private void OnDisable()
        {
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
}