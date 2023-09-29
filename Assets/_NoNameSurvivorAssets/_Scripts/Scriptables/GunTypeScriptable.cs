using UnityEngine;

namespace FikretGezer
{
    [CreateAssetMenu(fileName = "GunType", menuName = "Scriptables/Gun", order = 1)]
    public class GunTypeScriptable : ScriptableObject
    {
        public Sprite gunImage;
        public Mesh gunMesh;
        public Color gunColor;
        public float coolDown;
        public float damageAmount;
        // public float critique;
        // public float range;
        // public float pierce;        
    }
}
