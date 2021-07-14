using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField]private int sortingOrderBase = 20;
    // [SerializeField] Dictionary<Renderer,int> myRenderer;

    [SerializeField] SortingGroup sortingGroup;
    [SerializeField] private int offset = -10;
    private bool runOnlyOnce = false;
    #region
    /* private void Awake()
     {
         myRenderer = new Dictionary<Renderer, int>();

         if (gameObject.tag != "Player")
         {
             myRenderer.Add(gameObject.GetComponent<Renderer>(),
                 gameObject.GetComponent<Renderer>().sortingOrder);

         }
         else
         {
             foreach (Renderer childRenderer in transform.GetComponentsInChildren<Renderer>())
             {
                 myRenderer.Add(
                         childRenderer.GetComponent<Renderer>(),
                         childRenderer.GetComponent<Renderer>().sortingOrder);

             }
         }
     }

     private void LateUpdate()
     {

         foreach(KeyValuePair<Renderer,int> kvp in myRenderer)
         {
             int sortingOrder = sortingOrderBase + kvp.Value;
             kvp.Key.sortingOrder = (int)(sortingOrder - transform.position.y - offset); ;
         }

     }*/
    #endregion

    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();

    }

    private void LateUpdate()
    {
        
    }
}
