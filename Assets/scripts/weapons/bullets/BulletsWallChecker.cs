using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
//очень узкоспециализированный класс
//тебе нужно, чтобы проверялось не попадание пули в стену (что является частным случаем), а попадание пули КУДА-ЛИБО, а потом смотреть куда пуля попала и обрабатывать это
public class BulletsWallChecker : MonoBehaviour
{
    //ты не правильно поставил регион и не правильно используешь сериалайз филды, на них должны быть pragma warning (смотри в доке, что тебе Маша кидала)
    [SerializeField] private string tagForTrigget2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagForTrigget2D)
        {
            collision.gameObject.GetComponent<Bullet>().GetBackToParent();
            //collision.GetComponent<Bullet>().GetBackToParent(); - работает так же
        }
    }
}