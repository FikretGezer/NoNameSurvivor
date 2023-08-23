using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

namespace FikretGezer
{
    public class PoolingExercise : MonoBehaviour
    {
        [SerializeField] private WeirdObject _weirdObjectPrefab;
        [SerializeField] private TMP_Text _pooledCount;
        [SerializeField] private TMP_Text _activeCount;
        [SerializeField] private TMP_Text _inactiveCount;

        [SerializeField] private int _spawnCount = 10;
        [SerializeField] private int _defaultCapacity = 10;
        [SerializeField] private int _maxCapacity = 20;


        [SerializeField] private bool usePool;

        private ObjectPool<WeirdObject> _pool;
        
        private void Start() {
            _pool = new ObjectPool<WeirdObject>(Deneme, o => {
                o.gameObject.SetActive(true);
            }, o => {
                o.gameObject.SetActive(false);
            }, o => {
                Destroy(o.gameObject);
            }, false, _defaultCapacity, _maxCapacity);

            InvokeRepeating(nameof(Spawn), 0.2f, .2f);
        }
        private WeirdObject Deneme()
        {
            return Instantiate(_weirdObjectPrefab);
        }
        private void Update() {
            _pooledCount.text = $"Pooled: {_pool.CountAll}";
            _activeCount.text = $"Active: {_pool.CountActive}";
            _inactiveCount.text = $"Inactive: {_pool.CountInactive}";
        }
        private void Spawn()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                var o = usePool ? _pool.Get() : Instantiate(_weirdObjectPrefab);
                o.transform.position = transform.position + Random.insideUnitSphere * 10;
                o.Init(KillShape);
            }
        }
        private void KillShape(WeirdObject o)
        {
            if(usePool) _pool.Release(o);
            else Destroy(o.gameObject);
        }
    }
}
