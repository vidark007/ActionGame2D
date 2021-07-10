using System.Collections.Generic;
using UnityEngine;

namespace Env
{
    public interface ITerrainSeed
    {   
        string getName();
        GameObject getPlaceHolder();
        public List<GameObject> generate();
        Transform getTransform();
    }


}