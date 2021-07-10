using System.Collections.Generic;
using UnityEngine;

namespace Env
{
    public interface ISeedGenerator
    {   
        string getName();
        public List<GameObject> generate();
        Transform getTransform();
    }


}