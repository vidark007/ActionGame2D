using ActionGame.Combat;
using System.Collections;
using UnityEngine;
using System;

public class Fighter : MonoBehaviour
{
    CharacterIdentifier characterIdentifier;

    [SerializeField] float timerBetweenAttack;
    float timeSinceLastAttack = 0f;
    float timeSinceLastSummon = 0f;
    [SerializeField] float meleeAttackSpeed =8f;
    [SerializeField] Transform projectileSpawnPosition;

    [SerializeField] Pooler projectilePooler;
    Pooler mobPooler;

    Animator animator;

    Health target;
    private void Start()
    {
        characterIdentifier = GetComponent<CharacterIdentifier>();
      
        animator = GetComponent<Animator>();

        //for enemy we take component "Health" from Player
        //set time Between attack
        if (!characterIdentifier.IsPlayer())
        {
            if(GameObject.FindWithTag(InGameTags.Player.ToString()) == null) return;

            //SetTimerBetweenAttack(characterIdentifier.GetEnemyTimerBetweenAttack());

            target = GameObject.FindWithTag(InGameTags.Player.ToString()).GetComponent<Health>();
        }
/*        else if (characterIdentifier.IsPlayer())
        {
            timerBetweenAttack = characterIdentifier.GetWeapon_BetweenAttackTime();
        }*/

        if (characterIdentifier.IsCharacterDistanceClass() || characterIdentifier.IsPlayer())
        {
            FindAndSetChildrenComponent();
        }

        if (characterIdentifier.IsASummoner())
        {
            foreach (Transform child in transform.parent)
            {
                if (child.name == Pooler.PoolerTyp.MobPooler.ToString())
                {
                    mobPooler = child.GetComponent<Pooler>();
                }

            }
        }
    }

    public void SetTimerBetweenAttack(float time)
    {
        timerBetweenAttack = time;
    }

    public void FindAndSetChildrenComponent()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.name == "ProjectileSpawnPosition")
            {
                projectileSpawnPosition = child.GetComponent<Transform>();
            }
        }

        foreach (Transform child in transform.parent)
        {
            if (child.name == Pooler.PoolerTyp.ProjectilePooler.ToString())
            {
                projectilePooler = child.GetComponent<Pooler>();
            }

        }
    }

    public void BasicAttack()
    {
        if (characterIdentifier.IsPlayer())
        {
            if (BaseAttackIsReady())
            {

                GameObject projectile = projectilePooler.SpawnFromPool();

                projectile.GetComponent<Projectile>().SetProjectilValues(
                    characterIdentifier.GetCharacterDamage(), 
                    characterIdentifier.GetAttackRange(), 
                    projectileSpawnPosition);

                timeSinceLastAttack = Time.time + timerBetweenAttack;  
            }
        }
        else if (characterIdentifier.IsCharacterMeleeClass())
        {
            if (BaseAttackIsReady())
            {
                StartCoroutine(MeleeAttack());
                timeSinceLastAttack = Time.time + timerBetweenAttack;
            }
        }
        else if (characterIdentifier.IsCharacterDistanceClass())
        {
            if (!characterIdentifier.HasAnAttackAnimation())
            {
                if (BaseAttackIsReady())
                {
                    RangeAttack();

                    timeSinceLastAttack = Time.time + timerBetweenAttack;
                }

            }
            else
            {
                animator.SetTrigger("attack");
            }

            if (characterIdentifier.IsASummoner())
            {
                SummonAMob();
            }

        }
    }

    public void AttackTriggerAnimation()
    {       
        RangeAttack();
    }

    private void RangeAttack()
    {
        GameObject projectile = projectilePooler.SpawnFromPool();

        projectile.GetComponent<Projectile>().SetProjectilValues(characterIdentifier.GetCharacterDamage(), 20, projectileSpawnPosition);
    }

    private bool BaseAttackIsReady()
    {
        return Time.time >= timeSinceLastAttack;
    }

    private void SummonAMob()
    {
        float summonRange, summonTimer;
        
        characterIdentifier.GetSummoner(out summonRange, out summonTimer);
        
        if(Time.time >= timeSinceLastSummon)
        {

            GameObject innvocation = mobPooler.SpawnFromPool();
            innvocation.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f));

            innvocation.GetComponent<AIController>().SetEnemyToAggressif();
            timeSinceLastSummon = Time.time + summonTimer;
        }
    }

    IEnumerator MeleeAttack()
    {
        target.TakeDamage(characterIdentifier.GetCharacterDamage());
        Vector2 orginalPosition = transform.position;
        Vector2 targetPosition = target.transform.position;

        float percent = 0;
        while(percent <= 1)
        {
            percent += Time.deltaTime * meleeAttackSpeed;
            float formula = (-Mathf.Pow(percent,2) + percent) * 4;
            transform.position = Vector2.Lerp(orginalPosition, targetPosition, formula);

            yield return null;
        }
    }

}
