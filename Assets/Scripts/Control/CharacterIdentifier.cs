using ActionGame.Combat;
using ActionGame.Stats;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

#region RequireComponent
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SortingGroup))]

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Fighter))]
[RequireComponent(typeof(Mover))]
#endregion
public class CharacterIdentifier : MonoBehaviour
{
    private const string gameobjectsNameWithComponentCharacterIdentifier = "Character";
    CharacterConfigSO character;
    
    [SerializeField] EnemyConfigSO enemyConfig;
    [SerializeField] PlayerConfigSO playerConfig;

    [SerializeField] EnemyConfigSO.Charactertyp classTyp;
    [SerializeField] WeaponConfigSO weaponConfig = null;
    [SerializeField] float characterDamage;

    bool isPlayer = false;
    [SerializeField]  float attackRange = 0f;


    [SerializeField] bool isSummmoner = false;
    [SerializeField] float summonRange, summonCoolDownTimer;
    [SerializeField] GameObject innvocationPrefab;

    
    private void Awake()
    {
        //preventing Error => MagicString in Fighter
        SetThisGameobjectName();

        StartCoroutine(SetCharacterValuesOnAwake());

        if (IsWeaponCarrierCharacter())
        {
            AddWeaponReferenceAndDamage(null, GetComponentInChildren<DistanceWeapon>().GetEquippedWeapon());
        }
        else
        {
            AddWeaponReferenceAndDamage(null, null);
        }

    }

    private void Start()
    {
        PickupWeapon.onWeaponPickup += AddWeaponReferenceAndDamage;
    }

    private void OnDisable()
    {
        PickupWeapon.onWeaponPickup -= AddWeaponReferenceAndDamage;
    }

    private void SetThisGameobjectName()
    {
        gameObject.name = gameobjectsNameWithComponentCharacterIdentifier;
    }

    IEnumerator SetCharacterValuesOnAwake()
    {
        if (playerConfig != null)
        {
            character = playerConfig;
            isPlayer = playerConfig.isPlayer;
        }

        if (enemyConfig != null)
        {
            character = enemyConfig;
            classTyp = enemyConfig.GetCharacterClass();

            if (enemyConfig.IsSummoner())
            {
                SetSummoner();
            }
        }

        yield return null;
    } 

    public void AddWeaponReferenceAndDamage(GameObject weaponpreFab, WeaponConfigSO weaponConfig)
    {
        if (IsWeaponCarrierCharacter())
        {
            this.weaponConfig = weaponConfig;
        }
        CalculCharacterGlobalDamage();
    }

    public CharacterConfigSO GetCharacter()
    {
        return character;
    }

    public WeaponConfigSO GetCurrentWeapon()
    {
        return weaponConfig;
    }

    public bool IsWeaponCarrierCharacter()
    {
        return gameObject.tag == InGameTags.Player.ToString() || gameObject.tag == InGameTags.NPC.ToString();
    }

    private void CalculCharacterGlobalDamage()
    {
        if (IsWeaponCarrierCharacter())
        {
            if (weaponConfig != null)
            {
                characterDamage += weaponConfig.GetWeaponDamage();
                attackRange += weaponConfig.GetWeaponRange();
            }
        }
        if (character != null)
        {
            characterDamage += character.GetDamageCharacterAmount();
            attackRange += character.GetRangeofBaseAttack();
        }
    }

    private void SetSummoner()
    {
        isSummmoner = enemyConfig.IsSummoner(out this.summonRange, out this.summonCoolDownTimer, out this.innvocationPrefab);
    }

    public GameObject GetSummoner(out float summonRange, out float summonCoolDownTimer)
    {
        summonRange = this.summonRange;
        summonCoolDownTimer = this.summonCoolDownTimer;
        return innvocationPrefab;
    }

    public float GetCharacterDamage() => characterDamage;

    public bool IsPlayer() => isPlayer;

    public bool IsASummoner() => isSummmoner;

    public float GetEnemyTimerBetweenAttack() => enemyConfig.GetTimerBetweenAttack();

    public float GetAttackRange() => isPlayer ? weaponConfig.GetWeaponRange() : character.GetRangeofBaseAttack();

    public bool IsCharacterMeleeClass() => classTyp == EnemyConfigSO.Charactertyp.Melee;
    public bool IsCharacterDistanceClass() => classTyp == EnemyConfigSO.Charactertyp.Distance;

    public GameObject GetEnemiesProjectilePrefab() => enemyConfig.GetProjectilePrefab();

    public bool HasAnAttackAnimation() => enemyConfig.HasAnAttackAnimation();

    public float GetWeapon_BetweenAttackTime() => weaponConfig.GetTimerBetweenAttack();

}
