using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Env
{
    public class SeedsGeneratorFinder : MonoBehaviour
    {
        [SerializeField] bool activate = false;
        private void Awake()
        {
            if (activate)
            {
                List<ISeedGenerator> seeds = FindObjectsOfType<MonoBehaviour>()
                    .OfType<ISeedGenerator>()
                    .ToArray().ToList();
            
                //Debug.Log($"Found {seeds.Count} seeds to generate");
            
                foreach (ISeedGenerator terrainSeed in seeds)
                {
                    //Debug.Log($"Generating a {terrainSeed.getName()} seed at position {terrainSeed.getTransform().position}");
                    List<GameObject> generated = terrainSeed.generate();
                    //Debug.Log($"Generated {generated.Count} elements");
                }
            }
            else
            {
                //Debug.Log("Seed generator disabled");
            }
        }
    }
}