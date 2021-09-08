using ActionGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;

    GameObject UI;

    Vector3 startingScale;
    Vector3 staringPosition;

    public static event Action<GameObject, WeaponConfigSO> onWeaponPickup;

    GameObject playersEquipedWeapon;

    WeaponConfigSO weaponConfig = null;

    PlayerController playerController;

    [Header("Destroy after x Amout of Time (in s)")]
    [SerializeField] float destroyAfterAmountOfTime = 300f;


    private void Start()
    {
        startingScale = transform.localScale;
        staringPosition = transform.position;
        playerController = GameObject.FindWithTag(InGameTags.Player.ToString()).GetComponent<PlayerController>();

        weaponConfig = weaponPrefab.GetComponent<DistanceWeapon>().GetEquippedWeapon();
         
        GetUIcomponentInChild();
    }

    private void OnEnable()
    {
        DestroyGameObjectAfterAmountOfTime(destroyAfterAmountOfTime);
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

    void DestroyGameObjectAfterAmountOfTime(float timer)
    {
        Destroy(this.gameObject, timer);
    }

    private void OnMouseOver()
    {
        playerController.MouseIsOverUI(true);

        playersEquipedWeapon = GameObject.Find("WeaponHolder").transform.GetChild(0).gameObject.GetComponent<DistanceWeapon>().WeaponPickupPrefab();

        transform.localScale = startingScale + new Vector3(0.2f, 0.2f, 0);
        UI.SetActive(true);
    }

    private void OnMouseExit()
    {
        transform.localScale = startingScale;
        UI.SetActive(false);

        playerController.MouseIsOverUI(false);
    }

    private void OnMouseDown()
    {
        Instantiate(playersEquipedWeapon, staringPosition, Quaternion.identity);
        onWeaponPickup?.Invoke(weaponPrefab, weaponConfig);

        Destroy(this.gameObject);
    }

    public string GetPickupWeaponName() => weaponPrefab.name;
}
