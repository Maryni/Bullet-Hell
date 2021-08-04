using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Interfaces;

namespace Global.Settings
{
    [CreateAssetMenu(fileName = "ActiveObjectsSettings", menuName = "ActiveObjects/Settings", order = 5)]
    public class ActiveObjectsSettings : ScriptableObject, IActiveOnSceneObjectStats
    {
        #region private variables

#pragma warning disable
        [SerializeField] private int hp;
        [SerializeField] private int speed;
        [SerializeField] private float damage;
        [SerializeField] private int intellegence;

#pragma warning restore

        #endregion private variables

        #region properties

        public int HP => hp;
        public float Speed => speed;
        public float Damage => damage;
        public int Intellegence => intellegence;

        #endregion properties
    }
}