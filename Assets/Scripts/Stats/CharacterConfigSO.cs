using UnityEngine;

namespace ActionGame.Stats
{
    public class CharacterConfigSO : ScriptableObject
    {
        [Header("Character Base Stats (Default - HEalth, Range and Damage -Value")]
        [SerializeField] protected float baseHealth;
        [SerializeField] protected int baseRangeCapacity;
        [SerializeField] protected int baseDamageAmount;

        [Header("Character Base Stats (Default Base Attack CoolDown (Timer)")]
        [SerializeField] protected float attackCoolDownTimer;

        public float GetDamageCharacterAmount()
        {
            return baseDamageAmount;
        }

        public float ReturnBaseHealth()
        {
            return baseHealth;
        }

        public float GetRangeofBaseAttack() => baseRangeCapacity;

        public float GetTimerBetweenAttack() => attackCoolDownTimer;


    }


}

