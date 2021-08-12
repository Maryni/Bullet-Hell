using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Coroutine coroutineCurrent;

    #endregion private variables

    #region Unity functions

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && coroutineCurrent == null && Time.timeScale >= 0)
        {
            coroutineCurrent = ValueMoveToZero();
        }
    }

    #endregion Unity functions

    #region public void

    public Coroutine ValueMoveToZero()
    {
        return StartCoroutine(DecreaseValueTimeScale());
    }

    public void ValueMoveFromZero()
    {
        StartCoroutine(IncreaseValueTimeScale());
    }

    #endregion public void

    #region private void

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

    #endregion private void
}