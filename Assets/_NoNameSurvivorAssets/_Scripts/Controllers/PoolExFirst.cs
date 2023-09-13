using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class PoolExFirst : PoolManagersBase
    {
        public static PoolExFirst Instance;
        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;
        }
    }
}
