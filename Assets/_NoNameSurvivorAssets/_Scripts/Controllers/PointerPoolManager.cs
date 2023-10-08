using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class PointerPoolManager : PoolManagersBase
    {
        public static PointerPoolManager Instance;

        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;
        }
        public override void ReturnAllToThePool()
        {
            foreach (GameObject item in items)
            {
                activeItems.Clear();
                item.SetActive(false);
                item.GetComponent<Animator>().ResetTrigger("trigPlaceholder");
            }
        }
    }
}
