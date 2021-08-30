using ActionGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWeaponStatsInfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoDamageText;
    [SerializeField] private TextMeshProUGUI infoDistanceText;
    [SerializeField] private TextMeshProUGUI infoWeaponCoolDownText;

    [SerializeField] WeaponConfigSO weaponConfig;

    private void Awake()
    {
        weaponConfig = Resources.Load<WeaponConfigSO>(GetComponent<PickupWeapon>().GetPickupWeaponName());

        Array.ForEach(transform.GetComponentsInChildren<Transform>(true), element =>
        {
            if (element.name == "Damage_TXT")
            {
                infoDamageText = element.GetComponent<TextMeshProUGUI>();
                infoDamageText.text = weaponConfig.GetWeaponDamage().ToString();
            }
            if (element.name == "Distance_TXT")
            {
                infoDistanceText = element.GetComponent<TextMeshProUGUI>();
                infoDistanceText.text = weaponConfig.GetWeaponRange().ToString();
            }
            if (element.name == "Cooldown_TXT")
            {
                infoWeaponCoolDownText = element.GetComponent<TextMeshProUGUI>();
                infoWeaponCoolDownText.text = weaponConfig.GetTimerBetweenAttack().ToString() + "s";
            }
        });
    }

}
