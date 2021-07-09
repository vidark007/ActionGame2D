using ActionGame.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    CharacterIdentifier characterIdentifier;

    Vector2 startingPosition;

    [SerializeField] float chaseDistance = 5f;
    [SerializeField] int suspicousTime = 5;
    WaitForSeconds suspicousTimer = new WaitForSeconds(8);
    [SerializeField] bool isAlerted = false;
    bool enemyWasHit = false;

    GameObject player;

    Mover mover;
    Fighter fighter;

    //Patrol
    float waypointTolerance = 1f;
    PatrolPath patrolPath;
    int currentWaypointIndex = 0;
    float timeSinceArrivedWayPoint = Mathf.Infinity;
    [SerializeField] float waypointDwellTime = 3f;
    [SerializeField] [Range(0f,10f)]float patrolingSpeed = 2f;

    private void Awake()
    {
        characterIdentifier = GetComponent<CharacterIdentifier>();
        player = GameObject.FindWithTag("Player");
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();

        if(gameObject.tag == "Enemy")
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
        characterIdentifier.onAIIsAInnvocation += SetEnemyToAggressif;
    }

    private void OnDisable()
    {
        characterIdentifier.onAIIsAInnvocation -= SetEnemyToAggressif;
    }

    private void FixedUpdate()
    {
        if(FriendIsAlerted())
        {

        }
        if (PlayerInView() || enemyWasHit)
        {
            if (!player.activeInHierarchy) return;

            EnemyBehaviourWhenTargetingPlayer();
        }
        else if (!PlayerInView() && isAlerted)
        {
            StartCoroutine("Timer_SetAlertedBehaviourOff");
        }
        else
        {
            PatrolBehaviour();
        }

        UpdatePatrolTimer();
    }

    private bool FriendIsAlerted()
    {
        return false;
    }

    private void UpdatePatrolTimer()
    {
        timeSinceArrivedWayPoint += Time.deltaTime;
    }

    private void EnemyBehaviourWhenTargetingPlayer()
    {
        if (isAlerted)
        {
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
        return GetDistanceToPlayer() <= chaseDistance;
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
            mover.Move(nextPosition, patrolingSpeed);
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
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }


}
