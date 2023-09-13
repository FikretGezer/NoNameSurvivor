using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class PoolExSecond : PoolManagersBase
    {
        public static PoolExSecond Instance;
        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;
        }
    }
}
