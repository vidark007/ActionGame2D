using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ActionGame.Combat
{
    public class DistanceWeapon : Weapon 
    {
        [SerializeField]
        WeaponConfigSO weapon;

        protected override void Awake()
        {
            base.Awake();

            weapon = Resources.Load<WeaponConfigSO>(weaponName.ToString());
        }

        private void OnEnable()
        {
            Fighter.onPlayerAttackEvent += AttackBehavior;
        }

        private void OnDisable()
        {
            Fighter.onPlayerAttackEvent -= AttackBehavior;
        }

        public override void AttackBehavior(float dammage,float range, bool isPlayer)
        {
            GameObject projectilePref = Pooler.Instance.SpawnFromPool();

            projectilePref.GetComponent<Projectile>().SetProjectilValues(dammage, range, gameObject.transform, isPlayer);
        }

        public override WeaponConfigSO GetEquippedWeapon()
        {
            return weapon;
        }
    }
}
