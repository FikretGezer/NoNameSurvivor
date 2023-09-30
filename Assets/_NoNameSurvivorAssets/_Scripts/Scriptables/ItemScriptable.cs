using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FikretGezer
{
    [CreateAssetMenu(fileName = "ItemType", menuName = "Scriptables/Item", order = 2)]
    public class ItemScriptable : ScriptableObject
    {
        public string itemName;
        public Image itemImage;
        public int itemHP;
        public int itemDamage;
        public int itemSpeed;
        public int itemAttackSpeed;
        public int itemLuck;
        public int itemArmor;
    }    
}
