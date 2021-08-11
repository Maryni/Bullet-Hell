using Global.ActiveObjects;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Global.Managers.Datas;

namespace Global.Managers
{
    public class SpawnManager : BaseManager
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private List<GameObject> listWeaponPrefabs;
        [SerializeField] private List<GameObject> listEnemyPrefabs; //+ list PowerUps

        [SerializeField] private Transform itemPool;
        [SerializeField] private Transform enemyPool;

        [SerializeField] private List<EnemyController> listEnemies;
        [SerializeField] private List<ItemInfo> listWeapons;
#pragma warning restore

        #endregion Inspector variables

        #region public void

        public void SpawnEnemy(EnemyType enemyType)
        {
            var objectEnemy = listEnemyPrefabs.FirstOrDefault(x => x.GetComponent<MeleeEnemy>().EnemyType == enemyType);
            var objectEnemyInit = Instantiate(objectEnemy, enemyPool);
            listEnemies.Add(objectEnemyInit.GetComponent<EnemyController>());
        }

        public void SpawnWeapon(WeaponType weaponType)
        {
            var objectWeapon = listWeaponPrefabs.FirstOrDefault(x => x.GetComponent<WeaponStats>().weaponType == weaponType);
            var objectWeaponInit = Instantiate(objectWeapon, itemPool);
            listWeapons.Add(objectWeaponInit.GetComponent<ItemInfo>());
        }

        public WeaponType GetRandomWeaponType() => (WeaponType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(WeaponType)).Length - 1);

        public EnemyType GetRandomEnemyType() => (EnemyType)UnityEngine.Random.Range(0, 4);

        public int GetLengthEnemyPrefabs() => listEnemyPrefabs.Count;

        public int GetLengthWeaponPrefabs() => listWeaponPrefabs.Count;

        public int GetLengthEnemySpawned() => listEnemies.Count;

        public int GetLengthWeaponSpawned() => listWeapons.Count;

        public override Type ManagerType => typeof(SpawnManager);

        #endregion public void

        protected override bool OnInit()
        {
            return true;
        }
    }
}