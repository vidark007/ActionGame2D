using UnityEngine;

namespace ActionGame.Stats
{
    public class CharacterConfigSO : ScriptableObject
    {
        [Header("Character Base Stats (Default - HEalth, Range and Damage -Value")]
        [SerializeField] protected float baseHealth;
        [SerializeField] protected int baseRangeCapacity;
        [SerializeField] protected int baseDamageAmount;

        public float GetDamageCharacterAmount()
        {
            return baseDamageAmount;
        }

        public float ReturnBaseHealth()
        {
            return baseHealth;
        }

        public float GetRangeofBaseAttack() => baseRangeCapacity;

    }


}

