using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    [SerializeField] float healingAmount = 5f;

    [Header("Destrroy after x Amout of Time (in s)")]
    [SerializeField] float destroyAfterAmountOfTime = 300f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == InGameTags.Player.ToString())
        {
            if (!collision.GetComponent<Health>().IsPlayerHealthFull())
            {
                collision.GetComponent<Health>().Heal(healingAmount);

                DestroyGameObjectAfterAmountOfTime(0f);
            }
        }

        
    }

    private void OnEnable()
    {
        DestroyGameObjectAfterAmountOfTime(destroyAfterAmountOfTime);
    }

    void DestroyGameObjectAfterAmountOfTime(float timer)
    {
        Destroy(this.gameObject, timer);
    }
}
