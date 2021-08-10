using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        [SerializeField] public EnemyType enemyType;
        [SerializeField] public int hpMaximum;
        [SerializeField] public float speed;
        [SerializeField] public int hpValueCurrent;
        [SerializeField] public float damage;
        [SerializeField] public int defence;
        [SerializeField] public int intellegence;
        [SerializeField] public float attackRate;
    }
}