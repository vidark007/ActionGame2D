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

        public override WeaponConfigSO GetEquippedWeapon()
        {
            return weapon;
        }
    }
}
