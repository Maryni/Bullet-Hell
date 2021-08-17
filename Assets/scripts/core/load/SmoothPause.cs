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
        [SerializeField] private float timeScaleSeconds;

#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private Coroutine coroutineDecreaseToOne;
        private float maxTimeScale;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            maxTimeScale = Time.timeScale;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EnablePause();
            }
        }

        #endregion Unity functions

        #region public void

        public void StartPause()
        {
            EnablePause();
        }

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
            var timeStep = maxTimeScale / timeScaleSeconds;
            var timeRate = timeScaleSeconds * timeStep;

            while (Time.timeScale > 0)
            {
                Time.timeScale -= timeStep;
                yield return new WaitForSecondsRealtime(timeRate);
            }

            panelMenu.SetActive(true);
            StopPauseCoroutine(DecreaseValueTimeScale(callback));
            callback?.Invoke();
        }

        private IEnumerator IncreaseValueTimeScale()
        {
            var timeStep = maxTimeScale / timeScaleSeconds;
            var timeRate = timeScaleSeconds * timeStep;

            StopCoroutine(DecreaseValueTimeScale());
            while (Time.timeScale < 1)
            {
                Time.timeScale += timeStep;
                yield return new WaitForSecondsRealtime(timeRate);
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