using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnController : MonoBehaviour
{
    private Transform player;

    [Header("Enemy List - Category of Enemies to spawn from building")]
    [SerializeField] List<GameObject> enemies;
    [Header("Wave number in ONE hit")]
    [SerializeField] int enemyWaveNumber = 2;
    [SerializeField] float timebetweenSpawn = 3f;

    
    [SerializeField]Transform building;

    [SerializeField] int currentWave = 1;
    [SerializeField] int maxWave = 5;

    [SerializeField] float timeSinceLastWaveCounter = 5f;
    float timeSinceLastWave = 0f;

    private void Awake()
    {
        player = GameObject.FindWithTag(InGameTags.Player.ToString()).transform;
        GetBuilding();
    }

    private void GetBuilding()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.GetComponent<BuildingIdentifier>() != null)
            {
                building = child.transform;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyBehavior(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyBehavior(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBehavior(collision);
    }


    private void EnemyBehavior(Collider2D collision)
    {
        if (collision.tag == InGameTags.Enemy.ToString())
        {
            if (collision.GetComponent<AIController>().GetIsAlerted() || collision.GetComponent<AIController>().GetWasHit())
            {
                Debug.Log("Enemey is alerted");
                StartSpwaningEnemies();
            }
        }
    }

    public void StartSpwaningEnemies()
    {
        if (currentWave <= maxWave && Time.time >= timeSinceLastWave)
        {
            StartCoroutine(StartNextWave());
            currentWave++;

            timeSinceLastWave = Time.time + timeSinceLastWave;
        }
    }

    public IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeSinceLastWave);
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < enemyWaveNumber; i++)
        {
            if (player == null) yield break;

            Transform enemy = enemies[UnityEngine.Random.Range(0, enemies.Count)].transform;

            if(enemy.GetComponent<DropController>() != null)
            {
                enemy.GetComponent<DropController>().enabled= false;
            }

            if(enemy.tag != InGameTags.EnemyInnvocation.ToString())
            {
                SetEnemyTagToInnvocationTag(enemy);
            }

            enemy = Instantiate(enemy, transform.position, transform.rotation);

            enemy.transform.parent = this.transform;

            yield return new WaitForSeconds(timebetweenSpawn);

        }
        
    }

    //Set enemy to aggressiv
    private static void SetEnemyTagToInnvocationTag(Transform enemy)
    {
        foreach (Transform child in enemy.transform)
        {
            if (child.name == "Character")
            {
                child.tag = InGameTags.EnemyInnvocation.ToString();

            }
        }
    }
}
