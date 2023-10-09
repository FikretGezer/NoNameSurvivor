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
        [HideInInspector] public int spawnIncreaser = 1;
        [HideInInspector] public bool gotPooledObject;

        private List<Vector3> locations = new List<Vector3>();
        private int totalSpawnCount;
        private float _sizeOfSpawnArea;
        private float elapsedTime;
        private bool isSpawned = true;

        private const float maxChance = 100f;
        private const float easyChance = 100f, mediumChance = 0f, hardChance = 0f;
        public List<float> chances;
        private bool isEnemyGot;
        public bool isRoundOver;
        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;

            //maxSpawnCount = 50;
            _sizeOfSpawnArea = _groundMeshRenderer.bounds.size.x / 2 - 30;

            chances.Add(easyChance);
            chances.Add(mediumChance);
            chances.Add(hardChance);
            StartNewWave();       
        }
        private void Update() {     
            if(!TimeManagement.Instance.isGameCompleted)
            {
                if(!gotPooledObject)
                {
                    StartCoroutine(SpawnTimer(timePerSpawn));
                }              
            }
        }        

        public void StartNewWave()
        {
            elapsedTime = 0f;
            totalSpawnCount = 0;
            maxSpawnCountPerRound += 10;
            spawnIncreaser = TimeManagement.Instance.CurrentLevel;
            gotPooledObject = true;
            locations.Clear();
            StopCoroutine(nameof(EnemySpawn));
            StopCoroutine(nameof(SpawnTimer));
            EnemySpawnChanceArranger();
            StartCoroutine(SpawnTimer(1f));
        }

        private void SpawnPointerOnSpawnPositions()
        {
            isSpawned = true;
            int min = 1 * spawnIncreaser;
            int max = 3 * spawnIncreaser;
            int spawnCount = Random.Range(min, max);

            spawnIncreaser++;
            //totalSpawnCount += count;
            locations.Clear();
            void ReturnThePointerWithPos(Vector3 pos)
            {
                GameObject pointer = PointerPoolManager.Instance.GetPooledObject();
                pointer.transform.position = pos;
                pointer.GetComponent<Animator>().SetTrigger("trigPlaceholder");
            }
            if (TimeManagement.Instance.currentTime > 1f && !isRoundOver)
            {
                for (int i = 0; i < spawnCount; i++)
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
        private void EnemySpawnChanceArranger()
        {
            TimeManagement.Instance.isNewRoundStarted = false;            
            var currentWave = TimeManagement.Instance.CurrentLevel;

            for(int i = 0; i < 3; i++)
            {
                if(chances[i] < 100f)
                {
                    var chanceVision = chances[i] + 2 * currentWave;
                    if(chanceVision < 100f)
                        chances[i] = chanceVision;
                    else 
                        chances[i] = 100f;                
                }
            }
        }
        private GameObject GetEnemy()
        {
            float chance = Random.Range(1, 101);
            int typeNumber = Random.Range(0, 3);
            float currentEnemyChance = easyChance;

            switch (typeNumber)
            {
                case 0:
                    currentEnemyChance = chances[0];
                    break;
                case 1:
                    currentEnemyChance = chances[1];
                    break;
                case 2:
                    currentEnemyChance = chances[2];
                    break;
            }
            if (chance < currentEnemyChance)
            {
                if (typeNumber == 0) return GetPooledObject();
                if (typeNumber == 1) return MediumEnemyPoolManager.Instance.GetPooledObject();
                if (typeNumber == 2) return HardEnemyPoolManager.Instance.GetPooledObject();
            }           

            return GetPooledObject();            
        }
        IEnumerator EnemySpawn(float delayBeforeSpawn) //delay should be exactly same with the particle effect duration/lifetime
        {
            yield return new WaitForSeconds(delayBeforeSpawn);
            if(!isRoundOver)
            {
                foreach (Vector3 position in locations)
                {
                    GameObject enemy = GetEnemy();
                    if(enemy != null)
                    {
                        enemy.transform.position = position;
                    }
                }
                isSpawned = false;
                PointerPoolManager.Instance.ReturnAllToThePool();
                gotPooledObject = false;
            }
        }
        IEnumerator SpawnTimer(float delay)
        {
            gotPooledObject = true;

            yield return new WaitForSeconds(delay);

            SpawnPointerOnSpawnPositions();            
        }
    }
}
