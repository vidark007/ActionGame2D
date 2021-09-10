using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    [SerializeField] List<Item> itemCharacteristics;

    [System.Serializable]
    public class Item
    {
        public float percentChance;
        public GameObject prefab;
        public int quantityMaxNumber;
    }

    public void InstiateLotItem()
    {
        Vector2 startingPosition = transform.position;

        float randomNbr = Random.Range(0, 100f);

        Debug.Log(randomNbr);

        foreach(Item item in itemCharacteristics)
        {
            if (randomNbr <= item.percentChance)
            {
                float quantityNumber = GenerateQuantityNumber(item);

                for (int i = 0; i < quantityNumber; i++)
                {
                    Vector2 spawnPosition = startingPosition + new Vector2(UnityEngine.Random.Range(-5.0f, 5.0f), UnityEngine.Random.Range(-5.0f, 5.0f));
                    Instantiate(item.prefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    private static float GenerateQuantityNumber(Item itemQuantity)
    {
        float quantity;

        if (itemQuantity.quantityMaxNumber != 1 && itemQuantity.quantityMaxNumber != 0)
        {
            quantity = Random.Range(1, itemQuantity.quantityMaxNumber);
        }
        else
        {
            quantity = itemQuantity.quantityMaxNumber;
        }
        return quantity;
    }
}
