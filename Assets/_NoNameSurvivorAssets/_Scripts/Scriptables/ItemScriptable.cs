using UnityEngine;

namespace FikretGezer
{
    [CreateAssetMenu(fileName = "ItemType", menuName = "Scriptables/Item", order = 2)]
    public class ItemScriptable : CardTypeScriptable
    {
        public string itemName;
        public Sprite itemImage;
        public int itemHP;
        public int itemDamage;
        public int itemSpeed;
        public int itemAttackSpeed;
        public int itemLuck;
        public int itemArmor;
        public int itemCost;
    }    
}
