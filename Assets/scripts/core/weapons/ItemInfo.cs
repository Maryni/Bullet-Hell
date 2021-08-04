using Global.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    #region private variables

#pragma warning disable
    [Header("0 - weapon, 1 - other thing"), SerializeField] private int typeItem;
    [SerializeField] private string value;
    private SpriteRenderer renderer;
    [SerializeField] private float timerValue;
    [SerializeField] private TimerController timer;
#pragma warning restore

    #endregion private variables

    #region public void

    public void SetTimeForTimer(float timerValue) => this.timerValue = timerValue;

    public string GetValue() => value;

    #endregion public void

    #region private void

    #region Unity function

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        if (typeItem == 0)
        {
            value = Random.Range(0, 3).ToString();
            name = "RandomWeapon";
        }
        else
        {
            value = "0";
            name = "Weapon_0";
        }
        renderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        timer.SetTimerValue(timerValue);
    }

    private void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            if (timer.GetTimerFinishedOnce())
            {
                gameObject.SetActive(false);
            }
        }
    }

    #endregion Unity function

    #endregion private void
}