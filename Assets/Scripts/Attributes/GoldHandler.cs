using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int goldAmount = 50;

    public void AddGold(int goldAmount)
    {
        this.goldAmount += goldAmount;
    }
}
