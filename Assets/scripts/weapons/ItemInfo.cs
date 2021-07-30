using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
public class ItemInfo : MonoBehaviour
{
    #region private variables
//ты не правильно поставил регион и не правильно используешь сериалайз филды, на них должны быть pragma warning (смотри в доке, что тебе Маша кидала)

    [SerializeField] private string name; //ты его не используешь нигде - удалить

    [Header("0 - weapon, 1 - other thing"), SerializeField]
    private int typeItem; //ты его не используешь нигде - удалить

    [SerializeField] private string value;
    private SpriteRenderer renderer;
    [SerializeField] private float timer;
    private float timerTemp;

    #endregion private variables

    #region public void

    //что это за валуе, какое оно, за что оно отвечает - под вопросом
    //не правильный нейминг
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

//Этот скрипт нарушает принцип единой ответственности
//скрипт не только хранит в себе информацию, он еще занимаеться каким-то таймингом
//В идеале это должна быть структура даных, которая что-то помещает, судя по названию класса