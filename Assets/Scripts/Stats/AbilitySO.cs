using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/New Ability", order = 2)]
public class AbilitySO: ScriptableObject
{
    [SerializeField] private bool isUltime =false;
    [SerializeField] private float damage = 0f;
    [SerializeField] private GameObject abilityPrefab;
    [SerializeField] private int rangeAdditionToBaseRange = 0;
}
