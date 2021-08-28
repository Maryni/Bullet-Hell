using UnityEngine;

namespace Global.Player
{
    public class Player : MonoBehaviour
    {
        #region private variables

#pragma warning disable

        [SerializeField] private int hpMaximum;
        [SerializeField] private int speed;
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
        }

        public void DecreaseHp(float hp)
        {
            hpValue -= hp;
        }

        public void SetPlayerStatFromData(int hp, int speed, int defence)
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