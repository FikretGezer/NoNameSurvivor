using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    [CreateAssetMenu(fileName = "GunType", menuName = "Scriptables/Gun", order = 1)]
    public class GunTypeScriptable : ScriptableObject
    {
        public float coolDown;
        public float damageAmount;
        public float critique;
        public float range;
        public float pierce;
    }
}
