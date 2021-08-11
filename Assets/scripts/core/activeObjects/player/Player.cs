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

        #region Unity functions

        private void Start()
        {
            Init();
            RestoreHPToMaxHP();
        }

        #endregion Unity functions

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
            Debug.Log($"Im [ {name} ] triggered, I take {damage}");
            GetDamage(damage);
            if (IsDead())
            {
                Debug.Log("Player die");
            }
        }

        #endregion public void

        #region private void

        private void RestoreHPToMaxHP()
        {
            hpValue = playerData.hpMaximum;
        }

        private void Init()
        {
            playerData = Services.GetManager<DataManager>().DynamicData.PlayerData;
        }

        private void GetDamage(int damage)
        {
            var hpDecrease = damage - playerData.defence;
            if (hpDecrease < 0)
            {
                hpDecrease = 0;
            }
            hpValue -= hpDecrease;
        }

        #endregion private void
    }
}