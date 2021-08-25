using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionGame.Combat
{
    [CreateAssetMenu(fileName ="Weapon",menuName ="Weapons/New Weapon",order =1)]
    public class WeaponConfigSO : ScriptableObject
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] GameObject projectile = null;
        [SerializeField] float weaponRange = 0f;
        [SerializeField] float weaponDamage = 2f;

        [Header("Weapon Base Stats (Weapon CoolDown (Timer)")]
        [SerializeField] protected float attackCoolDownTimer;

        public GameObject GetProjectile()
        {
            return projectile;
        }

        public float GetWeaponDamage() => weaponDamage;

        public Weapon GetWeapon() => weapon;
        public float GetWeaponRange() => weaponRange;
        public float GetTimerBetweenAttack() => attackCoolDownTimer;
    }
}