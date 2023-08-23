using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FikretGezer
{
    public class WeirdObject : MonoBehaviour
    {
        private Action<WeirdObject> _killAction;
        public void Init(Action<WeirdObject> killAction)
        {
            _killAction = killAction;
        }
        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.layer == 6)
            {
                //StartCoroutine(nameof(Timer));
                //_killAction(this);
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }
        }
        IEnumerator Timer()
        {
            _killAction(this);
            yield return new WaitForSeconds(1f);
        }
    }
}
