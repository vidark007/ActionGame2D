using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionGame.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float wayPointGizmosRadius = 0.3f;
        private void OnDrawGizmos()
        {
           for(int i =0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.green;

                Gizmos.DrawSphere(GetWaypoint(i), wayPointGizmosRadius);

                int j = GetNextIndex(i);
                
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));

            }
        }

        public int GetNextIndex(int i)
        {
            //Modular arithmetic Modulo
            return (i + 1) % transform.childCount;
        }

        public Vector2 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}