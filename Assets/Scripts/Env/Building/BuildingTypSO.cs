using UnityEngine;

namespace ActionGame.Env.Building
{
    public class BuildingTypSO : ScriptableObject
    {
        [SerializeField] GameObject prefab;
        [SerializeField] int health;

        public GameObject GetPrefab() => prefab;
        public int GetHealth() => health;
    }
}
