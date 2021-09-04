using ActionGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapManager : MonoBehaviour
{
    Fighter playerFighterComponent;
    private void Start()
    {
        playerFighterComponent = GameObject.FindWithTag(InGameTags.Player.ToString()).GetComponent<Fighter>();
        PickupWeapon.onWeaponPickup += InstiateNewWeapon;
    }

    private void OnDisable()
    {
        PickupWeapon.onWeaponPickup -= InstiateNewWeapon;
    }

    private void InstiateNewWeapon(GameObject weaponToEquip, WeaponConfigSO weaponConfig)
    {
        Instantiate(weaponToEquip, transform.GetChild(0).position, transform.GetChild(0).rotation).transform.parent = gameObject.transform;

        StartCoroutine(WeaponSwapExecutionOrder());
    }

    IEnumerator WeaponSwapExecutionOrder()
    {
        yield return DestroyOldWeapon();

        playerFighterComponent.FindAndSetChildrenComponent();
    }

    IEnumerator DestroyOldWeapon()
    {
        GameObject.Destroy(transform.GetChild(0).gameObject);
        yield return null;
    }
}
