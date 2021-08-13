using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Game.Component
{
    public class SmoothPause : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private GameObject panelMenu;
        [SerializeField] private float timeScaleStep;
        [SerializeField] private float timeScaleRate;

#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private Coroutine coroutineDecreaseToOne;

        #endregion private variables

        #region Unity functions

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EnablePause();
            }
        }

        #endregion Unity functions

        #region public void

        public void ValueMoveFromZero()
        {
            StartCoroutine(IncreaseValueTimeScale());
        }

        public void ResetTimeScale()
        {
            Time.timeScale = 1f;
        }

        #endregion public void

        #region private void

        private void EnablePause()
        {
            if (coroutineDecreaseToOne == null)
            {
                coroutineDecreaseToOne = StartCoroutine(DecreaseValueTimeScale());
            }
        }

        private IEnumerator DecreaseValueTimeScale(Action callback = null)
        {
            while (Time.timeScale > 0)
            {
                Time.timeScale -= timeScaleStep;
                if (Time.timeScale > 0 && Time.timeScale < timeScaleStep)
                {
                    Time.timeScale = 0f;
                }
                yield return new WaitForSecondsRealtime(timeScaleRate);
            }

            panelMenu.SetActive(true);
            StopPauseCoroutine(DecreaseValueTimeScale(callback));
            callback?.Invoke();
        }

        private IEnumerator IncreaseValueTimeScale()
        {
            StopCoroutine(DecreaseValueTimeScale());
            while (Time.timeScale < 1)
            {
                Time.timeScale += timeScaleStep;
                yield return new WaitForSecondsRealtime(timeScaleRate);
            }
        }

        private void StopPauseCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
            coroutineDecreaseToOne = null;
        }

        #endregion private void
    }
}