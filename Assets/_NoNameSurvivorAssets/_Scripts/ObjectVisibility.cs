using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public static class ObjectVisibility
    {
        public static bool IsObjectVisible(this UnityEngine.Camera @this, Renderer renderer)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(@this), renderer.bounds);
    }
    }
}
