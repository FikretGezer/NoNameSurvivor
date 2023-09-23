using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public enum EnemyTypes{
        easy,
        medium,
        hard,
    }
    public class EnemySpawnController : PoolManagersBase
    {
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
        private int spawnIncreaser = 1;
        private bool isSpawned = true;

        private const float maxChance = 100f;

        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;

            //maxSpawnCount = 50;
            _sizeOfSpawnArea = _groundMeshRenderer.bounds.size.x / 2 - 5;
            StartNewWave();
        }
        
        private void Update() {            
            if(!gotPooledObject)
            {
                StartCoroutine(SpawnTimer(timePerSpawn));
            }                 
        }
        private void LateUpdate() {
            bool isThereEnemy = GameObject.FindWithTag("enemy");
            if(!isThereEnemy && !isSpawned)
            {
                elapsedTime = 0f;
                SpawnPointerOnSpawnPositions();
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
            isSpawned = true;
            int min = 1 * spawnIncreaser;
            int max = 3 * spawnIncreaser;
            int count = Random.Range(min, max);

            //int count = 1;
            spawnIncreaser++;
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
        
        private GameObject GetEnemy()
        {
            float chance = Random.Range(1, 101);
            while (true)
            {
                GameObject rnd = objectPrefabs[Random.Range(0, objectPrefabs.Count)];
                if(chance < rnd.GetComponent<EnemyBase>().spawnChances){                    
                    return rnd;
                }                               
            }
        }
        IEnumerator EnemySpawn(float delayBeforeSpawn) //delay should be exactly same with the particle effect duration/lifetime
        {
            yield return new WaitForSeconds(delayBeforeSpawn);
            foreach (Vector3 position in locations)
            {
                GameObject enemy = GetPooledObjectWithType(EnemyTypes.easy);
                if(enemy != null)
                {
                    enemy.transform.position = position;
                }
            }
            isSpawned = false;
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
