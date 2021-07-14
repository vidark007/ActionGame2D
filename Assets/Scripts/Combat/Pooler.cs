using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    GameObject objectToPoolPrefab;
    [SerializeField] List<GameObject> objectPoolersList;
    [SerializeField] protected int amount = 10;
    [Header("Pooler Typ :")]
    [SerializeField] private PoolerTyp poolerTyp;
    public static Pooler Instance { get; private set;}

    public enum PoolerTyp
    {
        ProjectilePooler,
        MobPooler
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //projectilePool = new List<GameObject>();
        SetPoolerGameobject();

        if (objectToPoolPrefab != null)
        {
            objectPoolersList = GenerateProjectilePool(amount);
        }
    }

    private void SetPoolerGameobject()
    {
        if (poolerTyp == PoolerTyp.ProjectilePooler)
        {
            CharacterIdentifier characterIdentifier = GetComponentInParent<CharacterIdentifier>();

            if (characterIdentifier.IsPlayer())
            {
                InstaniatePrefabToPool(characterIdentifier.GetCurrentWeapon().GetProjectile());
            }
            else
            {
                if (characterIdentifier.IsCharacterDistanceClass())
                {
                    GameObject projectile = characterIdentifier.GetEnemiesProjectilePrefab();
                    InstaniatePrefabToPool(projectile);
                }
            }
        }
        
        else if(poolerTyp == PoolerTyp.MobPooler)
        {
           
            int siblingIndex = GameObject.Find("SummonerBody").transform.GetSiblingIndex();
            CharacterIdentifier characterIdentifier = transform.parent.GetChild(siblingIndex).GetComponent<CharacterIdentifier>();
 
            if(characterIdentifier != null)
            {
                GameObject innvocation = characterIdentifier.GetSummoner(out float range, out float coolDown);
                InstaniatePrefabToPool(innvocation);
            }

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

            if(poolerTyp == PoolerTyp.ProjectilePooler)
            {
                Projectile projectile = objectToAddInPool.GetComponent<Projectile>();
                
                projectile.SetIsPlayerComponent(
                    GetComponentInParent<CharacterIdentifier>().IsPlayer());
            }
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
