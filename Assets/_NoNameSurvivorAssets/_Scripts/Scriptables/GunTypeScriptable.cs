using UnityEngine;

namespace FikretGezer
{
    [CreateAssetMenu(fileName = "GunType", menuName = "Scriptables/Gun", order = 1)]
    public class GunTypeScriptable : CardTypeScriptable
    {
        public string gunName;        
        public Sprite gunImage;
        public Mesh gunMesh;
        public Color gunColor;
        public float cooldown;
        public int damageAmount;
        public int gunCost;
        // public float critique;
        // public float range;
        // public float pierce;        
    }
}
