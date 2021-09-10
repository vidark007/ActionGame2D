using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ActionGame.Stats;

public class Health : MonoBehaviour
{
    [SerializeField]  float healthPoints;
    [SerializeField] float maxHealth;

    [SerializeField] UnityEvent onDamage;
    [SerializeField] UnityEvent onDieEvent;

    bool isDead = false;

    private void Start()
    {
        SetHealtOnAwake();
    }
    private void OnEnable()
    {
        if (isDead)
        {
            isDead = false;
            SetHealtOnAwake();
        }       
    }

    private void SetHealtOnAwake()
    {
        if(GetComponent<CharacterIdentifier>() != null)
        {
            healthPoints = GetComponent<CharacterIdentifier>().GetCharacter().ReturnBaseHealth();
            maxHealth = healthPoints;
        }
        if (GetComponent<BuildingIdentifier>() != null)
        {
            healthPoints = GetComponent<BuildingIdentifier>().GetBuildHealth();
            maxHealth = healthPoints;
        }

    }

    public void TakeDamage(float dammageAmount)
    {
        healthPoints -= dammageAmount;

        LifeLimiter();

        if (healthPoints == 0)
        {
            Die();
        }
        else
        {
            onDamage.Invoke();
        }

    }

    private void LifeLimiter()
    {
        healthPoints = Mathf.Clamp(healthPoints, 0, maxHealth);
    }

    public void Heal(float healthToRestore)
    {
        healthPoints += healthToRestore;

        LifeLimiter();
    }

    public float GetHealthPoint()
    {
        return healthPoints;
    }

    public bool IsPlayerHealthFull()
    {
        return healthPoints == maxHealth; 
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        //play die animation

        onDieEvent?.Invoke();
    }


}
