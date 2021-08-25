using ActionGame.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropController))]
public class AIController : MonoBehaviour
{
    CharacterIdentifier characterIdentifier;

    [SerializeField] Vector2 startingPosition;
    Vector2 randomguardPosition ;

    [SerializeField] float alertRadius = 16f;
    [SerializeField] int suspicousTime = 5;
    [SerializeField] float chasingMaxDistance = 40f;
    WaitForSeconds suspicousTimer = new WaitForSeconds(8);
    [SerializeField] bool isAlerted = false;
    [SerializeField] bool enemyWasHit = false;

    GameObject player;

    Mover mover;
    Fighter fighter;

    //Patrol
    float waypointTolerance = 1f;
    PatrolPath patrolPath;
    int currentWaypointIndex = 0;
    float timeSinceArrivedWayPoint = Mathf.Infinity;
    [SerializeField] float waypointDwellTime = 3f;
    [SerializeField] [Range(0f,10f)]float patrolingSpeedDivisor  = 2f;

    private void Awake()
    {
        characterIdentifier = GetComponent<CharacterIdentifier>();
        player = GameObject.FindWithTag(InGameTags.Player.ToString());
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();

        if(gameObject.tag == InGameTags.Enemy.ToString())
        {
            patrolPath = transform.parent.GetComponentInChildren<PatrolPath>();
        }
        else
        {
            SetEnemyToAggressif();
        }
    }

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void OnEnable()
    {
        Vector2 startingPositionOnEnable = transform.position;
        randomguardPosition = startingPositionOnEnable + new Vector2(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f));
    }

    private void FixedUpdate()
    {
        if (player == null) return;
        //vpublic static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask);
        RaycastHit2D hits = Physics2D.CircleCast(transform.position, 12f,Vector2.up,12f, 8);

        if (PlayerInView() || enemyWasHit)
        {
            if (!player.activeInHierarchy) return;

            EnemyBehaviourWhenTargetingPlayer();
        }

        if(enemyWasHit && GetDistanceToPlayer() > chasingMaxDistance)
        {
            enemyWasHit = false;
            
        }
        if(GetDistanceToPlayer() < chasingMaxDistance)
        {
            AggrevateNearbyEnemies();
        }
        else if (!PlayerInView() && isAlerted)
        {
            StartCoroutine("Timer_SetAlertedBehaviourOff");
        }
        else
        {
            if (gameObject.tag != InGameTags.EnemyInnvocation.ToString())
            {
                PatrolBehaviour();
            }
            else
            {
                mover.Move(randomguardPosition, 1f);
            }
        }

        UpdatePatrolTimer();
    }

    private void AggrevateNearbyEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, alertRadius, Vector3.up);
        foreach (RaycastHit2D hit in hits)
        {
            AIController ai = hit.collider.GetComponent<AIController>();
            if (ai != null)
            {
                if (ai.GetIsAlerted() || ai.GetWasHit())
                {
                    SetEnemyToAggressif();
                }
            }
        }
    }

    private void UpdatePatrolTimer()
    {
        timeSinceArrivedWayPoint += Time.deltaTime;
    }

    private void EnemyBehaviourWhenTargetingPlayer()
    {
        if (isAlerted)
        {
            //Stop running Coroutine
            StopCoroutine("Timer_SetAlertedBehaviourOff");
        }
        if (!isAlerted)
        {
            isAlerted = true;
        }
       

        if (IsInAttackRange())
        {
            mover.StopEnemyMovingAnimation();
            fighter.BasicAttack();
        }
        else
        {
            mover.Move(player.transform.position, 1f);
        }
    }

    private bool PlayerInView()
    {
        return GetDistanceToPlayer() <= alertRadius;
    }

    IEnumerator Timer_SetAlertedBehaviourOff()
    {
        mover.StopEnemyMovingAnimation();
        yield return suspicousTimer;
        isAlerted = false;
        enemyWasHit = false;
    }

    public void SetEnemyToAggressif()
    {
        enemyWasHit = true;
    }

    private void PatrolBehaviour()
    {
        if (patrolPath == null) return; 

        Vector2 nextPosition = startingPosition;

        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedWayPoint = 0;
                CycleWaypoint();
            }

            nextPosition = GetCurrentWaypoint();
        }
        mover.StopEnemyMovingAnimation();

        if (timeSinceArrivedWayPoint > waypointDwellTime)
        {
            mover.Move(nextPosition, patrolingSpeedDivisor);
        }
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }


    private bool IsInAttackRange() => GetDistanceToPlayer() <= characterIdentifier.GetAttackRange();
    private float GetDistanceToPlayer() => Vector2.Distance(player.transform.position, transform.position);

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
    }

    public bool GetIsAlerted() => isAlerted;
    public bool GetWasHit() => enemyWasHit;

}
