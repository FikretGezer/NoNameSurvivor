using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class CubicHermiteSpline : MonoBehaviour
    {
        public Transform pointA;
        public Transform pointB;
        public Transform tangentA;
        public Transform tangentB;
        [Range(0,1f)]public float t = 0;
        private void OnDrawGizmos() {
            Vector2 posA = pointA.position;
            Vector2 posB = pointB.position;
            Vector2 posTangetA = tangentA.position;
            Vector2 posTangetB = tangentB.position;

            Gizmos.color = Color.cyan;
            
            Gizmos.DrawSphere(posA, 0.05f);
            Gizmos.DrawSphere(posB, 0.05f);
            Gizmos.DrawSphere(posTangetA, 0.05f);
            Gizmos.DrawSphere(posTangetB, 0.05f);

            Gizmos.DrawLine(posA, posTangetA);
            Gizmos.DrawLine(posB, posTangetB);

            Gizmos.color = Color.white;
            
            var pos = CalculateHermitePoint(posA, posB, posTangetA, posTangetB, t);
            Gizmos.DrawWireSphere(pos, 0.1f);

            var prev = CalculateHermitePoint(posA, posB, posTangetA, posTangetB, 0);
            int maxPoints = 32;
            for (int i = 1; i < maxPoints; i++)
            {
                float time = i / (float)(maxPoints-1f);
                var cur = CalculateHermitePoint(posA, posB, posTangetA, posTangetB, time);
                Gizmos.DrawLine(prev, cur);
                prev = cur;
            }

        }

        public Vector3 CalculateHermitePoint(Vector2 a, Vector2 b, Vector2 aTangent, Vector2 bTangent, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;

            // float h1 = 2 * t3 - 3 * t2 + 1;
            // float h2 = -2 * t3 + 3 * t2;
            // float h3 = t3 - 2 * t2 + t;
            // float h4 = t3 - t2;

            float px = t*t*t + t*t + t + 1;
            float pdx = t*t + t + 1;

            float h1 = t*t*t + t*t + t + 1;
            float h2 = t*t + t + 1;
            float h3 = t3 - 2 * t2 + t;
            float h4 = t3 - t2;
            

            Vector3 position = h1 * a + h2 * b + h3 * aTangent + h4 * bTangent;
            
            return position;
        }      
    }    
}
