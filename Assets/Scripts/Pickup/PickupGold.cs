using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGold : MonoBehaviour
{
    [SerializeField] int minGoldAmount = 2;
    [SerializeField] int maxGoldAmount = 10;

    int goldAmount;

    [Header("Destroy after x Amout of Time (in s)")]
    [SerializeField] float destroyAfterAmountOfTime = 600f;

    void Start()
    {
        GerateRandomAmountOfGold();
        DestroyGameObjectAfterAmountOfTime(destroyAfterAmountOfTime);
    }

    void GerateRandomAmountOfGold()
    {
        goldAmount = Random.Range(minGoldAmount, maxGoldAmount);
    }

    void DestroyGameObjectAfterAmountOfTime(float timer)
    {
        Destroy(this.gameObject, timer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == InGameTags.Player.ToString())
        {
            collision.GetComponent<GoldHandler>().AddGold(goldAmount);

            DestroyGameObjectAfterAmountOfTime(0f);
        }

    }
}
