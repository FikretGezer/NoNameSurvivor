using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class EnemySpawnController : MonoBehaviour
    {
        public static EnemySpawnController Instance;

        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private MeshRenderer _groundMeshRenderer;
        [SerializeField] private int _spawnCount = 0;
        [SerializeField] private float timePerSpawn = 1f;
        
        [HideInInspector] public List<GameObject> enemies = new List<GameObject>();
        [HideInInspector] public List<GameObject> selectedEnemies = new List<GameObject>();

        private float _sizeOfSpawnArea;
        private List<Vector3> locations = new List<Vector3>();

        private bool gotPooledObject;
        private int totalSpawnCount = 0;
        private int maxSpawnCount = 50;
        public int TotalSpawnCount {
            set => totalSpawnCount = 0;
        }
        private void Awake() {
            if(Instance == null) Instance = this;
            _sizeOfSpawnArea = _groundMeshRenderer.bounds.size.x / 2 - 5;
        }
        private void Start() {
            GameObject parent = new GameObject();
            parent.name = "Enemy Parent";
            for (int i = 0; i < _spawnCount; i++)
            {
                // float x = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                // float z = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                // var _enemy = Instantiate(_enemyPrefab, new Vector3(x, 1f, z), Quaternion.identity);
                var _enemy = Instantiate(_enemyPrefab);
                enemies.Add(_enemy);
                _enemy.SetActive(false);
                _enemy.transform.parent = parent.transform;
            }

            Invoke(nameof(SpawnPointerOnSpawnPositions), 1f);
        }
        private void Update() {
            
            if(!gotPooledObject && totalSpawnCount < maxSpawnCount)
            {
                StartCoroutine(SpawnTimer(timePerSpawn));
            }
        }
        private void ReturnPointerWithPos(Vector3 pos)
        {
            GameObject pointer = PointerPoolManager.Instance.GetPooledObject();
            pointer.transform.position = pos;
            pointer.GetComponent<Animator>().SetTrigger("trigPlaceholder");           
        }
        
        private void SpawnPointerOnSpawnPositions()
        {
            int count = Random.Range(5, 10);
            totalSpawnCount += count;
            locations.Clear();
            if(TimeManagement.Instance.currentTime > 1f)
            {
                for (int i = 0; i < count; i++)
                {
                    float x = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                    float z = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                    Vector3 loc = new Vector3(x, 0.01f, z); 
                    locations.Add(loc + new Vector3(0,1-0.01f,0));
                    ReturnPointerWithPos(loc);    
                }
                StartCoroutine(EnemySpawn(2f));
            }
        }
        IEnumerator EnemySpawn(float delayBeforeSpawn) //delay should be exactly same with the particle effect duration/lifetime
        {
            yield return new WaitForSeconds(delayBeforeSpawn);
            foreach (var position in locations)
            {
                var obj = GetPooledEnemy();
                if(obj != null)
                {
                    obj.SetActive(true);
                    obj.transform.position = position;
                }
            }
            PointerPoolManager.Instance.ReturnAllToThePool();
            
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

            SpawnPointerOnSpawnPositions();
            gotPooledObject = false;
        }
        public GameObject GetPooledEnemy()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                if(!enemies[i].activeInHierarchy)
                {
                    return enemies[i];
                }
            }   
            return null;
        }
        public void ReturnAll()
        {
            foreach (var enemy in enemies)
            {
                enemy.SetActive(false);
            }
            selectedEnemies.Clear();
        }
    }
}
