using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Shooting.BulletSpace
{
    public class BulletFactory : MonoBehaviour
    {
        #region private variables

#pragma warning disable

#pragma warning restore

        #endregion private variables

        #region public void

        /// <summary>
        /// In array
        /// </summary>
        /// <param name="array"></param>
        public GameObject[] SpawnObjectsForFillArray(GameObject[] array, Transform transformParent, GameObject gameObject)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Instantiate(gameObject, transformParent);
                var tempGO = transformParent.GetChild(i).gameObject;
                tempGO.SetActive(false);
                array[i] = tempGO;
            }
            return array;
        }

        public GameObject[] SpawnObjectsForFillArray(GameObject[] array, Transform transformParent, GameObject gameObject, int countPoolObjects)
        {
            if (array.Length < countPoolObjects && array.Length != 0 && array != null)
            {
                array = IncreasedArrayRange(array, countPoolObjects);
            }
            for (int i = 0; i < countPoolObjects - transformParent.childCount; i++)
            {
                Instantiate(gameObject, transformParent);
            }
            for (int i = 0; i < transformParent.childCount; i++)
            {
                var tempGO = transformParent.GetChild(i).gameObject;
                tempGO.SetActive(false);
                array[i] = tempGO;
            }
            return array;
        }

        public GameObject[] SpawnObjectByIndex(GameObject[] array, Transform transformParent, GameObject gameObject, int indexToSpawn, int indexChild, bool forceChangeObjectByLastIndex)
        {
            if (array.Length <= indexToSpawn)
            {
                array = IncreasedArrayRange(array, array.Length + 1);
            }
            Instantiate(gameObject, transformParent);
            var tempGO = transformParent.GetChild(indexChild).gameObject;
            tempGO.SetActive(false);
            if (forceChangeObjectByLastIndex || (!forceChangeObjectByLastIndex && array[indexToSpawn] == null))
            {
                array[indexToSpawn] = tempGO;
            }
            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="transformParent"></param>
        /// <param name="gameObjectб"></param>
        /// <param name="forceChangeObjectByLastIndex">if true, we set new object at lastObject place</param>
        /// <returns></returns>
        public GameObject[] SpawnObjectByLastIndex(GameObject[] array, Transform transformParent, GameObject gameObjectб, bool forceChangeObjectByLastIndex)
        {
            Instantiate(gameObject, transformParent);
            var tempGO = transformParent.GetChild(transformParent.childCount - 1).gameObject;
            tempGO.SetActive(false);
            if (forceChangeObjectByLastIndex || (!forceChangeObjectByLastIndex && array[array.Length - 1] == null))
            {
                array[array.Length - 1] = tempGO;
            }
            return array;
        }

        #region private void

        private GameObject[] IncreasedArrayRange(GameObject[] array, int countFinishedArrayLength)
        {
            var arrayTemp = array;
            array = new GameObject[countFinishedArrayLength];
            for (int i = 0; i < arrayTemp.Length; i++)
            {
                array[i] = arrayTemp[i];
            }
            return array;
        }

        #endregion private void

        #endregion public void
    }
}