using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FikretGezer
{
    public class TangentMath : MonoBehaviour
    {
        private const float TAU = 6.28318530718f;
        [Range(0, 360f)]
        public float angleDeg = 0f;
        public float turns;
        private void OnDrawGizmos()
        {
            Handles.DrawWireDisc(default, Vector3.forward, 1f);
            float angleRad = Mathf.Deg2Rad * angleDeg;
            float angTurns = (float)EditorApplication.timeSinceStartup;
            turns = angTurns;
            //Vector2 v = AngleToRadVector(angleRad);
            Vector2 v = AngleToRadVector(-0.1f * angTurns * TAU);
            Gizmos.DrawLine(default, v);

            Vector2 AngleToRadVector(float angleRad) => new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }
    }
}
