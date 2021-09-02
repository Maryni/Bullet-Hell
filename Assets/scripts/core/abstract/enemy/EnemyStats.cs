﻿using System.Collections;
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
        [SerializeField] public float hpValueCurrent;
        [SerializeField] public float damage;
        [SerializeField] public int defence;
        [SerializeField] public int intelligence;
        [SerializeField] public float attackRate;

        private void OnDisable()
        {
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
}