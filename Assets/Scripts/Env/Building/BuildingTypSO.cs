using UnityEngine;

namespace ActionGame.Env.Building
{
    [CreateAssetMenu(fileName = "NewBuilding", menuName = "Building/New Building", order = 3)]
    public class BuildingTypSO : ScriptableObject
    {
        [SerializeField] GameObject prefab;
        [SerializeField] int buildingHealth;

        public GameObject GetPrefab() => prefab;
        public int GetHealth() => buildingHealth;
    }
}
