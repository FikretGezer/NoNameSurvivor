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
    }
}
