using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters_AbilitiesList", menuName = "Abilities/Characters Ability's List", order = 2)]
public class AbilitiesListSO: ScriptableObject
{
    [SerializeField] private List<AbilitySO> abilitiesList;
}
