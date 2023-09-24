using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class MoneyTextPoolManager : PoolManagersBase
    {
        public static MoneyTextPoolManager Instance;

        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;
        }
    }
}
