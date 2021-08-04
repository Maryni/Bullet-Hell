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
        [SerializeField] private int countForSpawn;
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

        /// <summary>
        /// spawn default gameObject (item)
        /// </summary>
        public void Spawn()
        {
            if (!spawnList.Contains(itemExample))
            {
                for (int i = 0; i < countForSpawn; i++)
                {
                    Instantiate(itemExample, RandomSpawnPoint(), Quaternion.identity, spawnerPool.transform);
                    spawnList.Add(spawnerPool.transform.GetChild(i).gameObject);
                    spawnList[i].GetComponent<ItemInfo>().SetTimeForTimer(timerToHide);
                }
            }
            else
            {
                spawnList[spawnList.Count - 1].transform.position = RandomSpawnPoint();
                spawnList[spawnList.Count - 1].SetActive(true);
            }
        }

        /// <summary>
        /// Use Spawn() with select gameObject and countToSpawn(default = 1)
        /// </summary>
        /// <param name="gameObject">to spawn</param>
        /// <param name="countSpawn">default = 1</param>
        public void Spawn(GameObject gameObject, int countSpawn = 1)
        {
            if (countSpawn > 0)
            {
                countForSpawn = countSpawn;
            }
            else
            {
                countSpawn = 1;
                Spawn(gameObject, countSpawn);
            }
            itemExample = gameObject;
            Spawn();
        }

        #endregion public void

        #region private void

        private Vector2 RandomSpawnPoint()
        {
            float randX = Random.Range(planeSpriteRenderer.bounds.min.x, planeSpriteRenderer.bounds.max.x);
            float randY = Random.Range(planeSpriteRenderer.bounds.min.y, planeSpriteRenderer.bounds.max.y);
            return new Vector2(randX, randY);
        }

        #endregion private void
    }
}