using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnController : MonoBehaviour
{
    private Transform player;

    [Header("Enemy List - Category of Enemies to spawn from building")]
    [SerializeField] List<GameObject> enemies;
    
    [SerializeField]Transform building;

    [SerializeField] int currentWave = 0;
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
                StartSpwaningEnemies();
            }
        }
    }

    public void StartSpwaningEnemies()
    {
        if (currentWave <= maxWave && Time.time >= timeSinceLastWave)
        {
            //StartCoroutine(StartNextWave());
            SpawnWave();
            currentWave++;
            timeSinceLastWave = Time.time + timeSinceLastWaveCounter;
        }
    }

    private void SpawnWave()
    {

        Transform enemy = enemies[UnityEngine.Random.Range(0, enemies.Count)].transform;

        if (enemy.GetComponent<DropController>() != null)
        {
           // enemy.GetComponent<Health>().;
        }

        if (enemy.tag != InGameTags.EnemyInnvocation.ToString())
        {
            SetEnemyTagToInnvocationTag(enemy);
        }

        enemy = Instantiate(enemy, transform.position, transform.rotation);

        enemy.transform.parent = this.transform;

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
