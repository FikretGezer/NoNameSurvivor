using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class XPPoolManager : MonoBehaviour
    {
        public static List<GameObject> xpItems = new List<GameObject>();
        public static void ClearXpItems()
        {
            foreach (var xp in xpItems)
            {
                Destroy(xp);
            }
            xpItems.Clear();
        }
    }
}
