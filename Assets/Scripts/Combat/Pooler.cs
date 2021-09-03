using ActionGame.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] GameObject objectToPoolPrefab;
    [SerializeField] List<GameObject> objectPoolersList;
    [SerializeField] protected int amount = 10;
    [Header("Pooler Typ :")]
    [SerializeField] private PoolerTyp poolerTyp;
    CharacterIdentifier characterIdentifier;
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
        objectPoolersList = new List<GameObject>();

        FindCharacterIdentifierInMyHierarchy();

        SetMobOrProjectileInPoolForENEMYCharacter();
    }

    private void SetMobOrProjectileInPoolForENEMYCharacter()
    {
        
        if (poolerTyp == PoolerTyp.ProjectilePooler)
        {
            if (characterIdentifier != null)
            {
/*                if (characterIdentifier.IsPlayer())
                {
                    InstaniatePrefabToPool(characterIdentifier.GetCurrentWeapon().GetProjectile());
                }*/
                if (!characterIdentifier.IsPlayer())
                {
                    if (characterIdentifier.IsCharacterDistanceClass())
                    {
                        GameObject projectile = characterIdentifier.GetEnemiesProjectilePrefab();
                        InstaniatePrefabToPool(projectile);
                    }
                }
            }
        }
        else if (poolerTyp == PoolerTyp.MobPooler)
        {
            if (characterIdentifier.IsASummoner())
            {
                GameObject innvocation = characterIdentifier.GetSummoner(out float range, out float coolDown);
                InstaniatePrefabToPool(innvocation);
            }

        }
    }

    private void FindCharacterIdentifierInMyHierarchy()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.name == "Character")
            {
                characterIdentifier = child.GetComponent<CharacterIdentifier>();
            }

        }
    }

    public void InstaniatePrefabToPool(GameObject prefab)
    {
        objectToPoolPrefab = prefab;

        objectPoolersList = GenerateGameObjectPool(amount);
    }

    List<GameObject> GenerateGameObjectPool(int amount)
    {
       
        for(int i =0; i < amount; i++)
        {
            GameObject objectToAddInPool = CreateNewObjectToPool();
            objectPoolersList.Add(objectToAddInPool);

            if(poolerTyp == PoolerTyp.ProjectilePooler)
            {
                Projectile projectile = objectToAddInPool.GetComponent<Projectile>();
                
                projectile.SetIsPlayerComponent(
                    characterIdentifier.IsPlayer());
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
