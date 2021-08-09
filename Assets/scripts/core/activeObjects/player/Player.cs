using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Interfaces;
using Global.Managers.Datas;

namespace Global.Player
{
    public class Player : MonoBehaviour, IActiveOnSceneObjectStats
    {
        #region private variables

#pragma warning disable
        [SerializeField] private PlayerData playerData;
        [SerializeField] private int hpValue;
#pragma warning restore

        #endregion private variables

        #region properties

        public int HP => playerData.hpMaximum;
        public float Speed => playerData.speed;
        public int HPValue => hpValue;

        #endregion properties

        #region public void

        public bool IsDead()
        {
            if (hpValue < 0)
            {
                return true;
            }
            return false;
        }

        public void ObjectTriggered(int damage)
        {
            Debug.Log($"Im [ {name} ] triggered");
            GetDamage(damage);
            if (IsDead())
            {
                Debug.Log("Player die");
            }
        }

        #endregion public void

        #region private void

        #region Unity functions

        private void Start()
        {
            Init();
            RestoreHPToMaxHP();
        }

        #endregion Unity functions

        private void RestoreHPToMaxHP() => hpValue = playerData.hpMaximum;

        private void Init() => playerData = Services.GetManager<DataManager>().DynamicData.PlayerData;

        private void GetDamage(int damage)
        {
            hpValue -= damage;
        }

        #endregion private void
    }
}