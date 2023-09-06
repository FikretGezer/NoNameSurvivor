using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class EnemySpawnController : MonoBehaviour
    {
        public static EnemySpawnController Instance;

        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _spawnCount = 0;
        [SerializeField] private float _sizeOfSpawnArea = 20;
        [SerializeField] private MeshRenderer _groundMeshRenderer;
        [SerializeField] private float timePerSpawn = 1f;
        public List<GameObject> enemies = new List<GameObject>();
        public List<GameObject> selectedEnemies = new List<GameObject>();
        private bool gotPooledObject;
        
        private void Awake() {
            if(Instance == null) Instance = this;
            _sizeOfSpawnArea = _groundMeshRenderer.bounds.size.x / 2 - 5;
        }
        private void Start() {
            GameObject parent = new GameObject();
            parent.name = "Enemy Parent";
            for (int i = 0; i < _spawnCount; i++)
            {
                float x = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                float z = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                var _enemy = Instantiate(_enemyPrefab, new Vector3(x, 1f, z), Quaternion.identity);
                enemies.Add(_enemy);
                _enemy.SetActive(false);
                _enemy.transform.parent = parent.transform;
            }

            MakeActive();
        }
        private void Update() {
            
            if(!gotPooledObject)
            {
                StartCoroutine(SpawnTimer(timePerSpawn));
            }
        }
        private void MakeActive()
        {
            int count = Random.Range(10, 15);
            for (int i = 0; i < count; i++)
            {
                var obj = GetPooledObject();
                if(obj != null)
                {
                    obj.SetActive(true);
                    float x = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                    float z = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                    obj.transform.position = new Vector3(x, 1f, z);
                }
            }
        }
        IEnumerator SpawnTimer(float delay)
        {
            var elaspedTime = 0f;
            gotPooledObject = true;
            while(elaspedTime < delay)
            {
                elaspedTime += Time.deltaTime;
                yield return null;
            }
            MakeActive();
            gotPooledObject = false;
        }
        public GameObject GetPooledObject()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                if(!enemies[i].activeInHierarchy)
                {
                    return enemies[i];
                }
            }   
            return null;
            //return null;
        }
        // public void ReturnToThePool(GameObject pooledObject)
        // {
        //     pooledObject.SetActive(false);
        // }
    }
}
