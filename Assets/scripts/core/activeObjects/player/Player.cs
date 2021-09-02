using UnityEngine;

namespace Global.Player
{
    public class Player : MonoBehaviour
    {
        #region private variables

#pragma warning disable

        [SerializeField] private int hpMaximum;
        [SerializeField] private float speed;
        [SerializeField] private int defence;
        [SerializeField] private float hpValue;

#pragma warning restore

        #endregion private variables

        #region properties

        public int HPMaximum => hpMaximum;
        public float Speed => speed;
        public float HPValue => hpValue;
        public int Defence => defence;

        #endregion properties

        #region public void

        public void DecreaseHp(int hp)
        {
            hpValue -= hp;
            if (hpValue < 0)
            {
                hpValue = 0;
            }
        }

        public void DecreaseHp(float hp)
        {
            hpValue -= hp;
            if (hpValue < 0)
            {
                hpValue = 0;
            }
        }

        public void SetPlayerStatFromData(int hp, float speed, int defence)
        {
            hpMaximum = hp;
            this.defence = defence;
            this.speed = speed;
        }

        public void RestoreHPToMaxHP()
        {
            hpValue = hpMaximum;
        }

        #endregion public void
    }
}