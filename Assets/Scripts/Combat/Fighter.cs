using ActionGame.Combat;
using System.Collections;
using UnityEngine;
using System;

public class Fighter : MonoBehaviour
{
    CharacterIdentifier characterIdentifier;

    float timerBetweenBaseAttack;
    float timeSinceLastAttack = 0f;
    float timeSinceLastSummon = 0f;
    [SerializeField] float meleeAttackSpeed =8f;
    [SerializeField] Transform projectileSpawnPosition;
    [SerializeField] Pooler projectilePooler;


    Health target;
    private void Start()
    {
        characterIdentifier = GetComponent<CharacterIdentifier>();
        timerBetweenBaseAttack = characterIdentifier.GetTimeBetweenAttack();

        //for enemy we take component "Health" from Player
        if (!characterIdentifier.IsPlayer())
        {
            target = GameObject.FindWithTag("Player").GetComponent<Health>();

            if (characterIdentifier.IsCharacterDistanceClass())
            {
                SetShootPosition();
            }
        }
        if (characterIdentifier.IsPlayer())
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
                if (child.name == "ProjectilePooler")
                {
                    projectilePooler = child.GetComponent<Pooler>();
                }

            }
        }

    }


    private void SetShootPosition()
    {
        foreach (Transform child in transform)
        {

            if (child.name == "ProjectileSpawnPosition")
            {
                projectileSpawnPosition = child.GetComponent<Transform>();
            }
        }

        foreach (Transform child in transform.parent)
        {
            if (child.name == "ProjectilePooler")
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

                projectile.GetComponent<Projectile>().SetProjectilValues(characterIdentifier.GetCharacterDamage(), characterIdentifier.GetAttackRange(), projectileSpawnPosition);

                timeSinceLastAttack = Time.time + timerBetweenBaseAttack;
            }
        }
        else if (characterIdentifier.IsCharacterMeleeClass())
        {
            if (BaseAttackIsReady())
            {
                StartCoroutine(MeleeAttack());
                timeSinceLastAttack = Time.time + timerBetweenBaseAttack;
            }
        }
        else if (characterIdentifier.IsCharacterDistanceClass())
        {
            if (BaseAttackIsReady())
            {
                GameObject projectile = projectilePooler.SpawnFromPool();

                projectile.GetComponent<Projectile>().SetProjectilValues(characterIdentifier.GetCharacterDamage(), 20, projectileSpawnPosition);

                timeSinceLastAttack = Time.time + timerBetweenBaseAttack;
            }

            if (characterIdentifier.IsASummoner())
            {
                //SummonAMob();
            }
        }
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
            int moobPoolerIndex = GameObject.Find(Pooler.PoolerTyp.MobPooler.ToString())
                .transform.GetSiblingIndex();

            GameObject innvocation = transform.parent.GetChild(moobPoolerIndex).GetComponent<Pooler>().SpawnFromPool();
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
