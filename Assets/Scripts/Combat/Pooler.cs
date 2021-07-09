using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] GameObject objectToPoolPrefab;
    [SerializeField] List<GameObject> objectPoolersList;
    [SerializeField] protected int amount = 10;

    public static Pooler Instance { get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //projectilePool = new List<GameObject>();
        if (GetComponentInParent<CharacterIdentifier>().IsPlayer())
        {
            InstaniatePrefabToPool(GetComponentInParent<CharacterIdentifier>().GetCurrentWeapon().GetProjectile());
        }

        if (objectToPoolPrefab != null)
        {
             objectPoolersList = GenerateProjectilePool(amount);
        }
    }

    public void InstaniatePrefabToPool(GameObject prefab)
    {
        objectToPoolPrefab = prefab;
    }

    List<GameObject> GenerateProjectilePool(int amount)
    {
       
        for(int i =0; i < amount; i++)
        {
            GameObject objectToAddInPool = CreateNewObjectToPool();
            objectPoolersList.Add(objectToAddInPool);
        }
        return objectPoolersList;
    }

    private GameObject CreateNewObjectToPool()
    {
        GameObject newObject = Instantiate(objectToPoolPrefab);
        newObject.transform.parent = this.transform;
        newObject.SetActive(false);

        return newObject;       
    }


    public GameObject SpawnFromPool()
    {
        foreach(GameObject objects in objectPoolersList)
        {
            if (!objects.activeInHierarchy)
            {
                objects.SetActive(true);
                return objects;

            }
        }

        GameObject newObjectToAddInPool = CreateNewObjectToPool();
        objectPoolersList.Add(newObjectToAddInPool);

        return newObjectToAddInPool;
    }


}
