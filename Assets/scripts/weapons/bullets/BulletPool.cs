using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//нет неймспейса
//пул должен быть менеджером
public class BulletPool : MonoBehaviour
{
    #region private variables

    //ты не правильно поставил регион и не правильно используешь сериалайз филды, на них должны быть pragma warning (смотри в доке, что тебе Маша кидала)
    [SerializeField] private Transform[] parentTransforms;
    [SerializeField] private List<GameObject> bulletsType;
    [SerializeField] private GameObject[] bulletsAutomatic;
    [SerializeField] private GameObject[] bulletsShotgun;
    [SerializeField] private GameObject[] bulletsRocket;
    [SerializeField] private int countPoolObjects; //название не совсем понятное
    [SerializeField] private UnityEvent unityEvent; //если не используешь - удаляй

    #endregion private variables

    #region public void

    //повторение кода, нарушение принципа DRY
    //функция в функции? когда мы успели перейти на процедурное программирование? убери. (+ я не уверен, что ты сам это сделал (сори))
    public GameObject GetObject(int indexWeapon)
    {
        if (indexWeapon == 0)
        {
            for (int i = 0; i < bulletsAutomatic.Length; i++)
            {
                if (bulletsAutomatic[i].activeInHierarchy == false)
                {
                    return bulletsAutomatic[i];
                }

                if (CheckValue(bulletsAutomatic))
                {
                    InstanceObjectByTypeAndCount(indexWeapon, countPoolObjects + 20);
                }
            }
        }

        if (indexWeapon == 1)
        {
            for (int i = 0; i < bulletsShotgun.Length; i++)
            {
                if (bulletsShotgun[i].activeInHierarchy == false)
                {
                    return bulletsShotgun[i];
                }

                if (CheckValue(bulletsShotgun))
                {
                    InstanceObjectByTypeAndCount(indexWeapon, countPoolObjects + 20);
                }
            }
        }

        if (indexWeapon == 2)
        {
            for (int i = 0; i < bulletsRocket.Length; i++)
            {
                if (bulletsRocket[i].activeInHierarchy == false)
                {
                    return bulletsRocket[i];
                }

                if (CheckValue(bulletsRocket))
                {
                    InstanceObjectByTypeAndCount(indexWeapon, countPoolObjects + 20);
                }
            }
        }

        return null;

        bool CheckValue(GameObject[] array)
        {
            bool active = false;
            for (int i = 0; i < countPoolObjects; i++)
            {
                if (array[i].activeInHierarchy)
                {
                    active = true;
                }

                if (!array[i].activeInHierarchy)
                {
                    active = false;
                }

                if (!active)
                {
                    return active;
                }
            }

            return active;
        }
    }

    //DRY нарушаешь
    //KISS нарушаешь
    //проверь свой код, и как он работает
    //(подсказка: на сцене очень много неактивных пуль)
    public void InstanceObjectByTypeAndCount(int typeWeapon, int count = 20)
    {
        countPoolObjects = count;

        if (typeWeapon == 0)
        {
            bulletsAutomatic = new GameObject[countPoolObjects];
            for (int i = 0; i < countPoolObjects; i++)
            {
                Instantiate(bulletsType[typeWeapon], parentTransforms[typeWeapon]);
                bulletsAutomatic[i] = parentTransforms[typeWeapon].GetChild(i).gameObject;
                bulletsAutomatic[i].SetActive(false);
            }
        }

        if (typeWeapon == 1)
        {
            bulletsShotgun = new GameObject[countPoolObjects];
            for (int i = 0; i < countPoolObjects; i++)
            {
                Instantiate(bulletsType[typeWeapon], parentTransforms[typeWeapon]);
                bulletsShotgun[i] = parentTransforms[typeWeapon].GetChild(i).gameObject;
                bulletsShotgun[i].SetActive(false);
            }
        }

        if (typeWeapon == 2)
        {
            bulletsRocket = new GameObject[countPoolObjects];
            for (int i = 0; i < countPoolObjects; i++)
            {
                Instantiate(bulletsType[typeWeapon], parentTransforms[typeWeapon]);
                bulletsRocket[i] = parentTransforms[typeWeapon].GetChild(i).gameObject;
                bulletsRocket[i].SetActive(false);
            }
        }
    }

    #endregion public void

    #region private void

    private void Awake()    //Посмотри в доке оформление, и как должен код выглядеть
{                           //Посмотри в доке оформление, и как должен код выглядеть
        InstanceByDefault();//Посмотри в доке оформление, и как должен код выглядеть
}                           //Посмотри в доке оформление, и как должен код выглядеть

    private void InstanceByDefault()
    {
        bulletsAutomatic = new GameObject[countPoolObjects];
        bulletsShotgun = new GameObject[countPoolObjects];
        bulletsRocket = new GameObject[countPoolObjects];
        for (int i = 0; i < countPoolObjects; i++)
        {
            Instantiate(bulletsType[0], parentTransforms[0]);
            bulletsAutomatic[i] = parentTransforms[0].GetChild(i).gameObject;
            bulletsAutomatic[i].SetActive(false);
        }

        for (int i = 0; i < countPoolObjects; i++)
        {
            Instantiate(bulletsType[1], parentTransforms[1]);
            bulletsShotgun[i] = parentTransforms[1].GetChild(i).gameObject;
            bulletsShotgun[i].SetActive(false);
        }

        for (int i = 0; i < countPoolObjects; i++)
        {
            Instantiate(bulletsType[2], parentTransforms[2]);
            bulletsRocket[i] = parentTransforms[2].GetChild(i).gameObject;
            bulletsRocket[i].SetActive(false);
        }
    }

    #endregion private void
}

//У тебя этот скрипт, по сути, выступает не только пулом, который будет хранить какие-то объекты, а и фабрикой, которая это все создает,
//что нарушает принцип единой ответственности