using Global.Managers.Datas;
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
        [SerializeField] private GameObject losePanel;
        [SerializeField] private float timeScaleSeconds;

#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private Coroutine coroutineDecreaseToOne;
        private float minTimeScale = 0f;
        private float maxTimeScale;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            SetValueFromData();
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

        public void StartPauseWhenDead()
        {
            EnableDiePanel();
        }

        public void StartPause()
        {
            EnablePause();
        }

        public void ValueMoveFromZero()
        {
            StartCoroutine(IncreaseValueTimeScale(minTimeScale, maxTimeScale, timeScaleSeconds));
        }

        public void ResetTimeScale()
        {
            StopAllCoroutines();
            Time.timeScale = 1f;
        }

        #endregion public void

        #region private void

        private void SetValueFromData()
        {
            var data = Services.GetManager<DataManager>().DynamicData;
            timeScaleSeconds = data.PauseData.pauseTime;
        }

        private void EnablePause()
        {
            if (coroutineDecreaseToOne == null)
            {
                coroutineDecreaseToOne = StartCoroutine(DecreaseValueTimeScale(maxTimeScale, minTimeScale, timeScaleSeconds, false));
            }
        }

        private void EnableDiePanel()
        {
            losePanel.SetActive(true);
        }

        private IEnumerator DecreaseValueTimeScale(float start, float end, float time, bool isDie, Action callback = null)
        {
            float lastTime = Time.realtimeSinceStartup;
            float timer = 0.0f;

            while (timer < time)
            {
                Time.timeScale = Mathf.Lerp(start, end, timer / time);
                timer += (Time.realtimeSinceStartup - lastTime);
                lastTime = Time.realtimeSinceStartup;
                yield return null;
            }

            Time.timeScale = end;
            if (isDie)
            {
                losePanel.SetActive(true);
            }
            else
            {
                panelMenu.SetActive(true);
            }

            StopPauseCoroutine(DecreaseValueTimeScale(start, end, time, isDie, callback));
            callback?.Invoke();
        }

        private IEnumerator IncreaseValueTimeScale(float start, float end, float time)
        {
            float lastTime = Time.realtimeSinceStartup;
            float timer = 0.0f;

            while (timer < time)
            {
                Time.timeScale = Mathf.Lerp(start, end, timer / time);
                timer += (Time.realtimeSinceStartup - lastTime);
                lastTime = Time.realtimeSinceStartup;
                yield return null;
            }

            Time.timeScale = end;
        }

        private void StopPauseCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
            coroutineDecreaseToOne = null;
        }

        #endregion private void
    }
}