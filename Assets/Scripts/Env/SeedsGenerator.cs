using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Env
{
    public class SeedsGenerator : MonoBehaviour
    {
        [SerializeField] bool activate = false;
        private void Awake()
        {
            if (activate)
            {
                List<ITerrainSeed> seeds = FindObjectsOfType<MonoBehaviour>()
                    .OfType<ITerrainSeed>()
                    .ToArray().ToList();
            
                Debug.Log($"Found {seeds.Count} seeds to generate");
            
                foreach (ITerrainSeed terrainSeed in seeds)
                {
                    Debug.Log(
                        $"Generating a {terrainSeed.getName()} seed at position {terrainSeed.getTransform().position}");
                    InstantiateSeedChildren(terrainSeed);
                    Destroy(terrainSeed.getPlaceHolder());
                }
            }
            else
            {
                Debug.Log("Seed generator disabled");
            }


        }

        private static void InstantiateSeedChildren(ITerrainSeed terrainSeed)
        {
            foreach (GameObject original in terrainSeed.generate())
            {
                GameObject instantiate = Instantiate(original, terrainSeed.getTransform());
                Debug.Log(
                    $"Instantiated {instantiate.name} at position {instantiate.transform.position} with scale {instantiate.transform.localScale}"
                );
            }
        }
    }
}