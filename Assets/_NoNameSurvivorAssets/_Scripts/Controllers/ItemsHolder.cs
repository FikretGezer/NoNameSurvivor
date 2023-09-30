using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FikretGezer
{
    public class ItemsHolder : MonoBehaviour
    {
        public List<GunTypeScriptable> gunsScr = new List<GunTypeScriptable>();
        public List<ItemScriptable> itemsScr = new List<ItemScriptable>();

        
        public static ItemsHolder Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
    }
}
