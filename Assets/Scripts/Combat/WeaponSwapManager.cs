using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapManager : MonoBehaviour
{
    private void Start()
    {
        Pickup.onWeaponPickup += SwapWeapon;
    }

    private void OnDisable()
    {
        Pickup.onWeaponPickup -= SwapWeapon;
    }

    private void SwapWeapon(GameObject weaponToEquip)
    {
        Instantiate(weaponToEquip, transform.GetChild(0).position, transform.GetChild(0).rotation).transform.parent = gameObject.transform;
        GameObject.Destroy(transform.GetChild(0).gameObject);
    }
}
