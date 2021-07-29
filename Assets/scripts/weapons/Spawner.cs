using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region private variables

    [SerializeField] private GameObject spawnerPool;
    [SerializeField] private List<GameObject> spawnList;
    [SerializeField] private GameObject itemExample;
    [SerializeField] private int countForOneTimeSpawn;
    [SerializeField] private float timerForSpawn;
    [SerializeField] private Vector2 spawnPoint;

    #endregion private variables

    #region public void

    public void Spawn()
    {
        if (timerForSpawn <= 0)
        {
            if (!spawnList.Contains(itemExample))
            {
                for (int i = 0; i < countForOneTimeSpawn; i++)
                {
                    Instantiate(itemExample, RandomSpawnPoint(), Quaternion.identity, spawnerPool.transform);
                    spawnList.Add(spawnerPool.transform.GetChild(i).gameObject);
                }
            }
            else
            {
                spawnList[spawnList.Count - 1].transform.position = RandomSpawnPoint();
                spawnList[spawnList.Count - 1].SetActive(true);
            }
        }
    }

    #endregion public void

    #region private void

    private Vector2 RandomSpawnPoint()
    {
        Vector2 pos = new Vector2(itemExample.transform.position.x - itemExample.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2, itemExample.transform.position.y - itemExample.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        return pos;
    }

    #endregion private void
}