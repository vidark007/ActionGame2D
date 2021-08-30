using ActionGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapManager : MonoBehaviour
{
    private void Start()
    {
        PickupWeapon.onWeaponPickup += SwapWeapon;
    }

    private void OnDisable()
    {
        PickupWeapon.onWeaponPickup -= SwapWeapon;
    }

    private void SwapWeapon(GameObject weaponToEquip, WeaponConfigSO weaponConfig)
    {
        Instantiate(weaponToEquip, transform.GetChild(0).position, transform.GetChild(0).rotation).transform.parent = gameObject.transform;
        GameObject.Destroy(transform.GetChild(0).gameObject);
    }
}
