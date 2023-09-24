using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class XPTextPoolManager : PoolManagersBase
    {
        public static XPTextPoolManager Instance;

        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;
        }
    }
}
