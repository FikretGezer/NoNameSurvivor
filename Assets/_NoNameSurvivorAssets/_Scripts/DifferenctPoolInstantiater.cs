using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FikretGezer
{
    public class DifferenctPoolInstantiater : MonoBehaviour
    {
        [SerializeField] private GameObject weirdObj;
        [SerializeField] private TMP_Text _pooledCount;
        [SerializeField] private TMP_Text _activeCount;
        [SerializeField] private TMP_Text _inactiveCount;
        private void Update() {
            if(Input.GetMouseButton(0))
            {
                ObjectPoolManager.SpawnObject(weirdObj, transform.position);
            }
            _pooledCount.text = $"Pooled: {ObjectPoolManager.ObjectPools.Count}";
            //_inactiveCount.text = $"Inactive: {PooledObjectInfo.InactiveObjects.Count}";
        }
    }
}
