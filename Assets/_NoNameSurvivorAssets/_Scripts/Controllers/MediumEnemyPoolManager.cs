using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class MediumEnemyPoolManager : PoolManagersBase
    {
        public static MediumEnemyPoolManager Instance;

        public override void Awake()
        {
            base.Awake();
            if (Instance == null) Instance = this;
        }
    }
}
