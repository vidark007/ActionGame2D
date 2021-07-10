using System;
using System.Collections.Generic;

namespace Env.seeds
{
    public class TreePrefabAssetsNames 
    {
        public static readonly string BLUE_JUNGLE_TREE = "Prefab/Trees/Tree01";
        public static readonly string YELLOW_JUNGLE_TREE = "Prefab/Trees/Tree02";
        public static readonly string GREEN_EUROPEAN_TREE = "Prefab/Trees/Tree03";
        
        public static List<string> getValues()
        {
            return new List<string>
            {
                BLUE_JUNGLE_TREE,
                YELLOW_JUNGLE_TREE,
                GREEN_EUROPEAN_TREE
            };
        }
    }
}