using ActionGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab;

    [SerializeField] GameObject UI;

    [SerializeField] Vector3 startingScale;

    public static event Action<GameObject> onWeaponPickup;

    private void Start()
    {
        startingScale = transform.localScale;
        GetUIcomponentInChild();
    }

    private void GetUIcomponentInChild()
    {
        Array.ForEach(transform.GetComponentsInChildren<Transform>(true), element =>
        {
            if (element.name == "UI")
            {
                UI = element.gameObject;
            }
        });

    }

    private void OnMouseOver()
    {
        transform.localScale = startingScale + new Vector3(0.2f, 0.2f, 0);
        UI.SetActive(true);
    }

    private void OnMouseExit()
    {
        transform.localScale = startingScale;
        UI.SetActive(false);
    }

    private void OnMouseDown()
    {
        onWeaponPickup?.Invoke(pickupPrefab);
    }

    public string GetPickupWeaponName() => pickupPrefab.name;
}
