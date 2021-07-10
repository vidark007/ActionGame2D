using System.Collections.Generic;
using UnityEngine;

namespace Env.seeds
{
    public abstract class AbstractSeed: MonoBehaviour,ITerrainSeed
    {
        public string getName()
        {
            return GetType().Name;
        }

        public abstract GameObject getPlaceHolder();

        public abstract List<GameObject> generate();

        public Transform getTransform()
        {
            return transform;
        }
    }
}