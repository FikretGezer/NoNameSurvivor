using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class HitVFXPoolManager : PoolManagersBase
    {
        public static HitVFXPoolManager Instance;

        public override void Awake()
        {
            base.Awake();
            if (Instance == null) Instance = this;
        }
        public void StartCor(GameObject obj)
        {
            StartCoroutine(DissapearIt(obj));
        }
        IEnumerator DissapearIt(GameObject obj)
        {
            yield return new WaitForSeconds(1.5f);
            obj.SetActive(false);
        }
    }
}
