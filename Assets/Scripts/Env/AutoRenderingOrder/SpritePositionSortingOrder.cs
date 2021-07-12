using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField]private int sortingOrderBase = 20;
    private Renderer myRenderer;

    private void Awake()
    {
        if(gameObject.tag != "Player")
        {
            myRenderer = gameObject.GetComponent<Renderer>();
        }
        else
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                //Debug.Log(transform.GetChild(i).name);
            }
        }
    }

    private void LateUpdate()
    {
        if(myRenderer != null)
        {
           // myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y);
        }
    }
}
