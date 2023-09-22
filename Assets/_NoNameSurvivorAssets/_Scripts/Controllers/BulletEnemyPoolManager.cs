using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class BulletEnemyPoolManager : PoolManagersBase
    {
        public static BulletEnemyPoolManager Instance;

        public override void Awake() {
            base.Awake();
            if(Instance == null) Instance = this;
        }
    }
}
