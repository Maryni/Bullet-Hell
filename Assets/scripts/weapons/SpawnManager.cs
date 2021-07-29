using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region private variables

    [SerializeField] private Spawner spawner;
    [SerializeField] private float timer;
    [SerializeField] private bool needToSpawn;
    [SerializeField] private ShootManager shootManager;
    [SerializeField] private ItemInfo currentItemInfo;

    #endregion private variables

    #region properties

    public Spawner Spawner => spawner;

    #endregion properties

    #region public void

    public void DisableSpawn() => needToSpawn = false;

    #endregion public void

    #region private void

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentItemInfo = collision.gameObject.GetComponent<ItemInfo>();
        int value = int.Parse(currentItemInfo.GetValue());
        shootManager.ChangeWeaponType(value);
        collision.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        float timerTemp = timer;
        if (timerTemp <= 0 && needToSpawn)
        {
            spawner.Spawn();
            timerTemp = timer;
        }
        if (timerTemp > 0 && needToSpawn)
        {
            timerTemp -= 0.1f;
        }
    }

    #endregion private void
}