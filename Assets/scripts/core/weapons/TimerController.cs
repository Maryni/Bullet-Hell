using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Timer
{
    public class TimerController : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private float timer;
        private float timerTemp;
#pragma warning restore

        #endregion private variables

        #region public void

        public void SetTimerValue(float timerValue)
        {
            timer = timerValue;
            timerTemp = timer;
        }

        public bool GetTimerFinishedOnce()
        {
            if (timer > 0)
            {
                timer -= 0.1f;
            }
            if (timer < 0)
            {
                return true;
            }
            return false;
        }

        public bool GetTimerFinishedRepeating()
        {
            if (timer > 0)
            {
                timer -= 0.1f;
            }
            if (timer < 0)
            {
                timer = timerTemp;
                return true;
            }
            return false;
        }

        #endregion public void
    }
}