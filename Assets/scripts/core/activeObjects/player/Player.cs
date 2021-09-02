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

        //1. Сделай только одну функцию с флоатом, я тебе это говорил. Мало того, она должна быть в контроллере, не здесь
        //2. У тебя в любом случае тут нарушается инкапсуляция - сделай поле публичным и меняй его как хочешь - у тебя
                //сейчас все так же, просто реализовано через функции

        // public void DecreaseHp(int hp)
        // {
        //     hpValue -= hp;
        //     if (hpValue < 0)
        //     {
        //         hpValue = 0;
        //     }
        // }
        //
        // public void DecreaseHp(float hp)
        // {
        //     hpValue -= hp;
        //     if (hpValue < 0)
        //     {
        //         hpValue = 0;
        //     }
        // }

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