using ActionGame.Combat;
using System.Collections;
using UnityEngine;
using System;

public class Fighter : MonoBehaviour
{
    CharacterIdentifier characterIdentifier;

    float timerBetweenBaseAttack;
    [SerializeField] float timeSinceLastAttack = 0f;
    [SerializeField] float timeSinceLastSummon = 0f;
    [SerializeField] float meleeAttackSpeed =8f;

    public static event Action<float,float,bool> onPlayerAttackEvent;
    int counter = 0;


    Health target;
    private void Start()
    {
        characterIdentifier = GetComponent<CharacterIdentifier>();
        timerBetweenBaseAttack = characterIdentifier.GetTimeBetweenAttack();

        //for enemy we take component "Health" from Player
        if (!characterIdentifier.IsPlayer())
        {
            target = GameObject.FindWithTag("Player").GetComponent<Health>();
        }
    }



    public void BasicAttack()
    {

        if (characterIdentifier.IsPlayer())
        {
            if (BaseAttackIsReady())
            {

                onPlayerAttackEvent?.Invoke(characterIdentifier.GetCharacterDamage(), characterIdentifier.GetAttackRange(),true);
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
                GameObject projectile = gameObject.transform.Find("ProjectilePooler").GetComponent<Pooler>().SpawnFromPool();
                projectile.GetComponent<Projectile>().SetProjectilValues(characterIdentifier.GetCharacterDamage(), 20, gameObject.transform, false);

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
            GameObject innvocation = gameObject.transform.Find("MobPooler").GetComponent<Pooler>().SpawnFromPool();
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
