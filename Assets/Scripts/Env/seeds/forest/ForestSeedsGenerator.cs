using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Env.seeds.forest
{   
    
    public class ForestSeedsGenerator: AbstractSeedGenerator
    {
        [SerializeField] private int density = 10;
        
        public override List<string> getPrefabNames()
        {
            List<String> assetsNames = new List<string>();
            assetsNames.AddRange(TreePrefabAssetsNames.getValues());
            assetsNames.AddRange(GrassPrefabAssetsNames.getValues());
            return assetsNames;
        }
        
        public override List<GameObject> GenerateWithPrefab(Dictionary<string, GameObject> prefabs, Bounds spriteBounds)
        {
            List<GameObject> generated = new List<GameObject>();
            
            GameObject tree = prefabs[TreePrefabAssetsNames.YELLOW_JUNGLE_TREE];

            for (int i = 0; i < density; i++)
            {
                generated.Add(AddChild(tree, VectorUtils.RandomPosition(spriteBounds)));
            }

            return generated;
        }

      
    }
}