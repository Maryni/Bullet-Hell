using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
public class SpawnManager : MonoBehaviour
{
    #region private variables

    //ты не правильно поставил регион и не правильно используешь сериалайз филды, на них должны быть pragma warning (смотри в доке, что тебе Маша кидала)
    [SerializeField] private Spawner spawner;
    [SerializeField] private float timer;
    private float timerTemp;
    [SerializeField] private bool needToSpawn;
    [SerializeField] private ShootManager shootManager;
    [SerializeField] private ItemInfo currentItemInfo;
    [SerializeField] private int countSpawnInfo;

    #endregion private variables

    #region properties

    public Spawner Spawner => spawner;

    #endregion properties

    #region public void

    public void DisableSpawn() => needToSpawn = false;

    #endregion public void

    #region private void

    private void OnTriggerEnter2D(Collider2D collision) //в менеджере?
    {
        if (collision.tag == "Item")
        {
            currentItemInfo = collision.gameObject.GetComponent<ItemInfo>();
            int value = int.Parse(currentItemInfo.GetValue());
            shootManager.ChangeWeaponType(value);
            collision.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        timerTemp = timer;
    }

    private void FixedUpdate()
    {
        SpawnUntilCount(countSpawnInfo);
    }

    private void SpawnUntilCount(int count)
    {
        if (spawner.SpawnList.Count >= count)
        {
            needToSpawn = false;
        }

        if (timer > 0 && needToSpawn)
        {
            timer -= 0.1f;
        }

        if (timer <= 0 && needToSpawn)
        {
            spawner.Spawn();
            timer = timerTemp;
        }
    }

    #endregion private void
}