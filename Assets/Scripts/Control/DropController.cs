using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    [SerializeField] GameObject[] item;
    [SerializeField] float[] dropPercent;

    Dictionary<GameObject,float> itemList;

    private void Start()
    {
        SetItemDictionnaryValues();
    }

    private void SetItemDictionnaryValues()
    {
        itemList = new Dictionary<GameObject, float>();

        if (item.Length == dropPercent.Length)
        {
            for (int i = 0; i < item.Length; i++)
            {
                itemList.Add(item[i], dropPercent[i]);
            }
        }
        else
        {
            Debug.LogError("Itemlist and DropPercentList are not equal");
        }

    }

    public void InstiateLotItem()
    {
        Vector2 startingPosition = transform.position;

        int randomNbr = Random.Range(0, 100);

        foreach (KeyValuePair<GameObject, float> item in itemList)
        {
            if(randomNbr <= item.Value)
            {
                
                Vector2 spawnPosition = startingPosition + new Vector2(UnityEngine.Random.Range(-5.0f, 5.0f), UnityEngine.Random.Range(-5.0f, 5.0f));
                Instantiate(item.Key, spawnPosition, Quaternion.identity);
            }
        }
    }
    
}
