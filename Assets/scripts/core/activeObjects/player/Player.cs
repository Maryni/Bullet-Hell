using UnityEngine;

namespace Global.Player
{
    public class Player : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable

        [SerializeField] private int hpMaximum;
        [SerializeField] private float speed;
        [SerializeField] private int defence;

#pragma warning restore

        #endregion Inspector variables

        #region public variables

        public float hpValue;

        #endregion public variables

        #region properties

        public int HPMaximum => hpMaximum;
        public float Speed => speed;
        public int Defence => defence;

        #endregion properties

        #region public void

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