using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Settings;
using Global.Interfaces;

namespace Global.ActiveObjects
{
    public class EnemyBase : MonoBehaviour, IActiveOnSceneObjectStats
    {
        #region private variables

#pragma warning disable

        [SerializeField] private bool noLoadSettingsFromFile;
        [Space, SerializeField] private int hp;
        [SerializeField] private float speed;
        [SerializeField] private int hpValue;
        [SerializeField] private float damage;
        [SerializeField] private int intellegence;
        [SerializeField] private ShootController shootController;
#pragma warning restore

        #endregion private variables

        #region properties

        public int HP => hp;
        public float Speed => speed;
        public int HPValue => hpValue;
        public float Damage => damage;
        public int Intellegence => intellegence;

        #endregion properties

        #region public void

        public void LoadStatValues(ActiveObjectsSettings settings)
        {
            if (!noLoadSettingsFromFile)
            {
                hp = settings.HP;
                speed = settings.Speed;
                damage = settings.Damage;
                intellegence = settings.Intellegence;
            }
            if (noLoadSettingsFromFile)
            {
                if (hp == 0)
                {
                    hp = settings.HP;
                }
                if (speed == 0)
                {
                    speed = settings.Speed;
                }
                if (damage == 0)
                {
                    damage = settings.Damage;
                }
                if (intellegence == 0)
                {
                    intellegence = settings.Intellegence;
                }
            }
        }

        #endregion public void
    }
}