using System;
using System.Collections.Generic;

namespace Env.seeds
{
    public class GrassPrefabAssetsNames
    {
        public static readonly string GREEN_SIMPLE_GRASS = "Prefab/Plants/Grass01";
        public static readonly string GREEN_TALL_GRASS = "Prefab/Plants/Grass02";
        public static readonly string GREEN_LARGE_GRASS = "Prefab/Plants/Grass03";


        public static List<string> getValues()
        {
            return new List<string>
            {
                GREEN_SIMPLE_GRASS,
                GREEN_TALL_GRASS,
                GREEN_LARGE_GRASS
            };
        }
    }
}