using UnityEngine;

namespace FikretGezer
{
    [CreateAssetMenu(fileName = "GunType", menuName = "Scriptables/Gun", order = 1)]
    public class GunTypeScriptable : ScriptableObject
    {
        public string gunName;        
        public Sprite gunImage;
        public Mesh gunMesh;
        public Color gunColor;
        public float coolDown;
        public int damageAmount;
        public int itemCost;
        // public float critique;
        // public float range;
        // public float pierce;        
    }
}
