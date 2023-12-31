using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class MoneyPoolManager : PoolManagersBase
    {
        public static MoneyPoolManager Instance;

        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;
        }
    }
}
