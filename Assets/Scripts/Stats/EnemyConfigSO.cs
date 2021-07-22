using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ActionGame.Stats
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Characters/New Enemy", order = 1)]
    public class EnemyConfigSO : CharacterConfigSO
    {
        [Header("Character Typ : Able of Summoning")]
        [SerializeField] protected bool canSummon;

        [Header("Character Class : Melee - Distance")]
        [SerializeField] private Charactertyp  characterClass;

        [Header("Attack Animation")]
        [SerializeField] private bool hasAnAttackAnimation = false;

        #region SummonerCharacteristics_Only
        [HideInInspector] [SerializeField] private float summonRange;
        [HideInInspector] [SerializeField] private float summonTimer = 5f;
        [HideInInspector] [SerializeField] private GameObject invocation;
        #endregion


        [HideInInspector] [SerializeField] GameObject projectile;
        public enum Charactertyp
        {
            Melee,
            Distance,
        }

        public Charactertyp GetCharacterClass()
        {
            return characterClass;
        }

        public bool IsSummoner() => canSummon;

        public bool HasAnAttackAnimation() => hasAnAttackAnimation;

        public bool IsSummoner(out float range, out float timer, out GameObject invocation)
        {
            range = this.summonRange;
            timer = this.summonTimer;
            invocation = this.invocation;

            return canSummon;
        }
        #region Summon_Editor 
#if UNITY_EDITOR
        [CustomEditor(typeof(EnemyConfigSO))]
        public class SummonAbilityEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                               
                serializedObject.Update();
                base.OnInspectorGUI();

                EnemyConfigSO enemyCharacertistcs = (EnemyConfigSO)target;
                if (enemyCharacertistcs.canSummon)
                {
                    DrawSummonerDetails(enemyCharacertistcs);
                }

                if (enemyCharacertistcs.characterClass == Charactertyp.Distance)
                {
                    DrawProjectileDetails(enemyCharacertistcs);
                }

                serializedObject.ApplyModifiedProperties();
            }

            private void DrawSummonerDetails(EnemyConfigSO summoner)
            {
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Summoner Characteristics");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("summonRange"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("summonTimer"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("invocation"));
            }


            private void DrawProjectileDetails(EnemyConfigSO distanceEnemey)
            {
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Projectile Prefab");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("projectile"));
            }


        }
#endif
        #endregion

        public GameObject GetProjectilePrefab() => projectile;
    }
}
