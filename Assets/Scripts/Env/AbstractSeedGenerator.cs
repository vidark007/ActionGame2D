using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Env.seeds
{
    public abstract class AbstractSeedGenerator : MonoBehaviour,ISeedGenerator
    {
        protected SpriteRenderer PlaceHolder;
        private void Awake()
        {
            PlaceHolder = GetComponent<SpriteRenderer>();
        }

        public string getName()
        {
            return name;
        }
        
        public List<GameObject> generate()
        {
            List<GameObject> generated = GenerateWithPrefab(preload(getPrefabNames()), PlaceHolder.sprite.bounds);
            
            PlaceHolder.enabled = false;
            return generated;
        }
        

        private Dictionary<string,GameObject> preload(List<string> prefabNames)
        {
            Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
            
            prefabNames.ForEach(prefabName =>
            {
                GameObject loaded = Resources.Load<GameObject>(prefabName);
                if (loaded == null)
                {
                    throw new Exception("Unable to load asset " + prefabName);
                }
                prefabs.Add(prefabName, loaded);
            });

            return prefabs;
        }

        public Transform getTransform()
        {
            return transform;
        }
        
        public abstract List<GameObject> GenerateWithPrefab(Dictionary<string, GameObject> prefabs, Bounds spriteBounds);
        
        protected GameObject AddChild(GameObject child, Vector3 localPosition)
        {
            GameObject instantiated = Instantiate(child, transform);
            instantiated.transform.localPosition = localPosition;
            var parentLocalScale = transform.localScale;
            instantiated.transform.localScale =
                new Vector3(1 / parentLocalScale.x, 1 / parentLocalScale.y, 1 / parentLocalScale.z);
            return instantiated;
        }

        public abstract List<string> getPrefabNames();
    }
}