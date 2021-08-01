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
    [SerializeField] private float timer;
    private float timerTemp;
#pragma warning restore

    #endregion private variables

    #region public void

    public void SetTimer(float timer)
    {
        this.timer = timer;
        timerTemp = this.timer;
    }

    public string GetValue() => value;

    #endregion public void

    #region private void

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        value = Random.Range(0, 3).ToString();
        name = "Item";
        renderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        timerTemp = timer;
    }

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= 0.1f;
        }
        if (timer < 0)
        {
            timer = timerTemp;
            gameObject.SetActive(false);
        }
    }

    #endregion private void
}