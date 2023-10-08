using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class HardEnemyPoolManager : PoolManagersBase
    {
        public static HardEnemyPoolManager Instance;

        public override void Awake()
        {
            base.Awake();
            if (Instance == null) Instance = this;
        }
    }
}
