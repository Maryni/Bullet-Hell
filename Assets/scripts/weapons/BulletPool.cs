using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletPool : MonoBehaviour
{
    #region private variables

    [SerializeField] private Transform parentTransform;
    [SerializeField] private List<GameObject> bulletsType;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private int countPoolObjects;
    [SerializeField] private int typeWeapon;
    [SerializeField] private UnityEvent unityEvent;

    #endregion private variables

    #region public void

    public GameObject GetObject()
    {
        for (int i = 0; i < countPoolObjects; i++)
        {
            if (bullets[i].activeInHierarchy == false)
            {
                return bullets[i];
            }
            if (CheckValue())
            {
                InstanceObjectByTypeAndCount(FindObjectOfType<ShootManager>().WeaponType, countPoolObjects * 2);
            }
        }
        return null;

        bool CheckValue()
        {
            bool active = false;
            for (int i = 0; i < countPoolObjects; i++)
            {
                if (bullets[i].activeInHierarchy)
                {
                    active = true;
                }
                if (!bullets[i].activeInHierarchy)
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

    public void InstanceObjectByTypeAndCount(int typeWeapon, int count = 20)
    {
        unityEvent.Invoke();
        this.typeWeapon = typeWeapon;
        countPoolObjects = count;
        bullets = new GameObject[countPoolObjects];
        for (int i = 0; i < countPoolObjects; i++)
        {
            Instantiate(bulletsType[typeWeapon], parentTransform);
            bullets[i] = parentTransform.GetChild(i).gameObject;
            bullets[i].SetActive(false);
        }
    }

    #endregion public void

    #region private void

    private void Awake()
    {
        unityEvent.AddListener(DropTrashBulletsAndRemove);
        InstanceByDefault();
    }

    private void InstanceByDefault()
    {
        typeWeapon = FindObjectOfType<ShootManager>().WeaponType;
        bullets = new GameObject[countPoolObjects];
        for (int i = 0; i < countPoolObjects; i++)
        {
            Instantiate(bulletsType[typeWeapon], parentTransform);
            bullets[i] = parentTransform.GetChild(i).gameObject;
            bullets[i].SetActive(false);
        }
    }

    private void DropTrashBulletsAndRemove()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }
    }

    #endregion private void
}