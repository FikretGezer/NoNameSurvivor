using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class EnemySpawnController : PoolManagersBase
    {
        // public static EnemySpawnController Instance;

        // [SerializeField] private GameObject _enemyPrefab;
        // [SerializeField] private MeshRenderer _groundMeshRenderer;
        // [SerializeField] private float timePerSpawn = 1f;
        // [SerializeField] private int _spawnCount = 0;
        
        // [HideInInspector] public List<GameObject> enemies = new List<GameObject>();
        // [HideInInspector] public List<GameObject> selectedEnemies = new List<GameObject>();

        // private float _sizeOfSpawnArea;
        // private List<Vector3> locations = new List<Vector3>();

        // private bool gotPooledObject;
        // private int totalSpawnCount = 0;
        // private int maxSpawnCount = 50;
        // public int TotalSpawnCount {
        //     set => totalSpawnCount = 0;
        // }
        // private void Awake() {
        //     if(Instance == null) Instance = this;
        //     _sizeOfSpawnArea = _groundMeshRenderer.bounds.size.x / 2 - 5;
        // }
        // private void Start() {
        //     GameObject parent = new GameObject();
        //     parent.name = "Enemy Parent";
        //     for (int i = 0; i < _spawnCount; i++)
        //     {
        //         // float x = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
        //         // float z = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
        //         // var _enemy = Instantiate(_enemyPrefab, new Vector3(x, 1f, z), Quaternion.identity);
        //         var _enemy = Instantiate(_enemyPrefab);
        //         enemies.Add(_enemy);
        //         _enemy.SetActive(false);
        //         _enemy.transform.parent = parent.transform;
        //     }

        //     Invoke(nameof(SpawnPointerOnSpawnPositions), 1f);
        // }
        // private void Update() {
            
        //     if(!gotPooledObject && totalSpawnCount < maxSpawnCount)
        //     {
        //         StartCoroutine(SpawnTimer(timePerSpawn));
        //     }
        // }
        // private void ReturnPointerWithPos(Vector3 pos)
        // {
        //     GameObject pointer = PointerPoolManager.Instance.GetPooledObject();
        //     pointer.transform.position = pos;
        //     pointer.GetComponent<Animator>().SetTrigger("trigPlaceholder");           
        // }
        
        // private void SpawnPointerOnSpawnPositions()
        // {
        //     int count = Random.Range(5, 10);
        //     totalSpawnCount += count;
        //     locations.Clear();
        //     if(TimeManagement.Instance.currentTime > 1f)
        //     {
        //         for (int i = 0; i < count; i++)
        //         {
        //             float x = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
        //             float z = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
        //             Vector3 loc = new Vector3(x, 0.01f, z); 
        //             locations.Add(loc + new Vector3(0,1-0.01f,0));
        //             ReturnPointerWithPos(loc);    
        //         }
        //         StartCoroutine(EnemySpawn(2f));
        //     }
        // }
        // IEnumerator EnemySpawn(float delayBeforeSpawn) //delay should be exactly same with the particle effect duration/lifetime
        // {
        //     yield return new WaitForSeconds(delayBeforeSpawn);
        //     foreach (var position in locations)
        //     {
        //         var obj = GetPooledEnemy();
        //         if(obj != null)
        //         {
        //             obj.SetActive(true);
        //             obj.transform.position = position;
        //         }
        //     }
        //     PointerPoolManager.Instance.ReturnAllToThePool();
            
        // }
        // IEnumerator SpawnTimer(float delay)
        // {
        //     var elaspedTime = 0f;
        //     gotPooledObject = true;
        //     while(elaspedTime < delay)
        //     {
        //         elaspedTime += Time.deltaTime;
        //         yield return null;
        //     }

        //     SpawnPointerOnSpawnPositions();
        //     gotPooledObject = false;
        // }
        // public GameObject GetPooledEnemy()
        // {
        //     for (int i = 0; i < _spawnCount; i++)
        //     {
        //         if(!enemies[i].activeInHierarchy)
        //         {
        //             return enemies[i];
        //         }
        //     }   
        //     return null;
        // }
        // public void ReturnAll()
        // {
        //     foreach (var enemy in enemies)
        //     {
        //         enemy.SetActive(false);
        //     }
        //     selectedEnemies.Clear();
        // }

        public static EnemySpawnController Instance;

        [Header("Current Script's Parameters")]
        [SerializeField] private MeshRenderer _groundMeshRenderer;
        [SerializeField] private float timePerSpawn = 1f;
        [SerializeField] private int maxSpawnCountPerRound;

        [HideInInspector] public List<GameObject> selectedEnemies = new List<GameObject>();

        private List<Vector3> locations = new List<Vector3>();
        private bool gotPooledObject;
        private int totalSpawnCount;
        private float _sizeOfSpawnArea;
        private float elapsedTime;

        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;

            //maxSpawnCount = 50;
            _sizeOfSpawnArea = _groundMeshRenderer.bounds.size.x / 2 - 5;
            StartNewWave();
        }
        
        private void Update() {
            
            if(!gotPooledObject && totalSpawnCount < maxSpawnCountPerRound)
            {
                StartCoroutine(SpawnTimer(timePerSpawn));
            }
        }

        public void StartNewWave()
        {
            elapsedTime = 0f;
            totalSpawnCount = 0;
            maxSpawnCountPerRound += 10;
            Invoke(nameof(SpawnPointerOnSpawnPositions), 1f);
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
                    ReturnThePointerWithPos(loc);    
                }
                StartCoroutine(EnemySpawn(2f)); //Same amount of delay with the effect playing time
            }
        }
        private void ReturnThePointerWithPos(Vector3 pos)
        {
            GameObject pointer = PointerPoolManager.Instance.GetPooledObject();
            pointer.transform.position = pos;
            pointer.GetComponent<Animator>().SetTrigger("trigPlaceholder");           
        }
        IEnumerator EnemySpawn(float delayBeforeSpawn) //delay should be exactly same with the particle effect duration/lifetime
        {
            yield return new WaitForSeconds(delayBeforeSpawn);
            foreach (Vector3 position in locations)
            {
                GameObject enemy = GetPooledObject();
                if(enemy != null)
                {
                    enemy.transform.position = position;
                }
            }
            PointerPoolManager.Instance.ReturnAllToThePool();            
        }
        IEnumerator SpawnTimer(float delay)
        {
            elapsedTime = 0f;

            gotPooledObject = true;
            while(elapsedTime < delay)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            SpawnPointerOnSpawnPositions();
            gotPooledObject = false;
        }
    }
}
