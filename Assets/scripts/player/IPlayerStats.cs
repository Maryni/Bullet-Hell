using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса

//смысла в этом интерфейс вообще нет, ты его никак не испозуешь кроме реализации класса плеера
public interface IPlayerStats
{
    int HP { get; }
    float Speed { get; }
}