using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Controllers.Spawn
{
    public class Spawner : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private GameObject spawnerPool;
        [SerializeField] private List<GameObject> spawnList;
        [SerializeField] private GameObject itemExample;
        [SerializeField] private int countForOneTimeSpawn;
        [SerializeField] private Vector2 spawnPoint;
        [SerializeField] private SpriteRenderer planeSpriteRenderer;
        [SerializeField] private float timerToHide;
#pragma warning restore

        #endregion private variables

        #region properties

        public List<GameObject> SpawnList => spawnList;

        #endregion properties

        #region public void

        public void SetTimer(float timer) => timerToHide = timer;

        public void Spawn()
        {
            if (!spawnList.Contains(itemExample))
            {
                for (int i = 0; i < countForOneTimeSpawn; i++)
                {
                    Instantiate(itemExample, RandomSpawnPoint(), Quaternion.identity, spawnerPool.transform);
                    spawnList.Add(spawnerPool.transform.GetChild(i).gameObject);
                    spawnList[i].GetComponent<ItemInfo>().SetTimer(timerToHide);
                }
            }
            else
            {
                spawnList[spawnList.Count - 1].transform.position = RandomSpawnPoint();
                spawnList[spawnList.Count - 1].SetActive(true);
            }
        }

        #endregion public void

        #region private void

        private Vector2 RandomSpawnPoint()
        {
            float randX = Random.Range(planeSpriteRenderer.bounds.min.x, planeSpriteRenderer.bounds.max.x);
            float randY = Random.Range(planeSpriteRenderer.bounds.min.y, planeSpriteRenderer.bounds.max.y);
            Vector2 pos = new Vector2(randX, randY);
            return pos;
        }

        #endregion private void
    }
}