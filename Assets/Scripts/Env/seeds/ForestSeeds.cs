using System.Collections.Generic;
using UnityEngine;

namespace Env.seeds
{
    public class ForestSeeds: AbstractSeed
    {
        public override GameObject getPlaceHolder()
        {
            return null;
        }

        public override List<GameObject> generate()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            for (int i = 0; i < 10; i++)
            {
                GameObject loaded = Resources.Load<GameObject>("Prefab/Trees/Tree01");
                gameObjects.Add(loaded);
            }
            return gameObjects;
        }

    }
}