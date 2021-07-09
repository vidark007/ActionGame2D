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
    public static event Action onDieEvent;

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
        healthPoints = GetComponent<CharacterIdentifier>().GetCharacter().ReturnBaseHealth();
        maxHealth = healthPoints;
    }

    public void TakeDamage(float dammageAmount)
    {
        healthPoints -= dammageAmount;
        healthPoints = Mathf.Clamp(healthPoints, 0, maxHealth);    

        if (healthPoints == 0)
        {
            Die();
        }
        else
        {
            onDamage.Invoke();
        }

    }

    public void Heal(float healthToRestore)
    {
        
    }

    public float GetHealthPoint()
    {
        return healthPoints;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Dead");
        gameObject.SetActive(false);
        //play die animation

        onDieEvent?.Invoke();
    }


}