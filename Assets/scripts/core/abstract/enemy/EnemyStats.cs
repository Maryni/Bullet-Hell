using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        /*[SerializeField]*/  public EnemyType enemyType; //они и так публичные, зачем сериализировать?
        /*[SerializeField]*/  public int hpMaximum; //они и так публичные, зачем сериализировать?
        /*[SerializeField]*/  public float speed; //они и так публичные, зачем сериализировать?
        /*[SerializeField]*/  public float hpValueCurrent; //они и так публичные, зачем сериализировать?
        /*[SerializeField]*/  public float damage; //они и так публичные, зачем сериализировать?
        /*[SerializeField]*/  public int defence; //они и так публичные, зачем сериализировать?
        /*[SerializeField]*/  public int intelligence; //они и так публичные, зачем сериализировать?
        /*[SerializeField]*/  public float attackRate; //они и так публичные, зачем сериализировать?

        //Оно не будет работать в релизе
        //Бахнешь билд - будет проблема такая же, как было
        //Сериализируй этот класс и сохраняй в плеер префс
        private void OnDisable()
        {
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
}