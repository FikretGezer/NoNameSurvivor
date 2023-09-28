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

        public override void Awake()
        {
            base.Awake();
            if(Instance == null) Instance = this;

            //maxSpawnCount = 50;
            _sizeOfSpawnArea = _groundMeshRenderer.bounds.size.x / 2 - 5;
            StartNewWave();       

            chances.Add(easyChance);
            chances.Add(mediumChance);
            chances.Add(hardChance);
        }
        private void Update() {     
            if(FindObjectOfType<PlayerController>())
            {
                if(!gotPooledObject)
                {
                    StartCoroutine(SpawnTimer(timePerSpawn));
                }       
                if(TimeManagement.Instance.isNewRoundStarted)       
                {
                    EnemySpawnChanceArranger();
                }         
            }       
        }
        private void LateUpdate() {
            if(FindObjectOfType<PlayerController>()){
                bool isThereEnemy = GameObject.FindWithTag("enemy");
                if(!isThereEnemy && !isSpawned)
                {
                    elapsedTime = 0f;
                    SpawnPointerOnSpawnPositions();
                }            
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
            isEnemyGot = true;
            return GetPooledObjectWithType(EnemyTypes.easy);
            // GameObject rnd;
            // EnemyTypes type = default;
            // int typeNumber;
            // float currentEnemyChance = easyChance;
            
            // while (isEnemyGot)
            // {
            //     typeNumber = Random.Range(0, 3);
            //     switch(typeNumber)
            //     {
            //         case 0:
            //             currentEnemyChance = chances[0];
            //             type = EnemyTypes.easy;
            //             break;
            //         case 1:
            //             currentEnemyChance = chances[1];
            //             type = EnemyTypes.medium;
            //             break;
            //         case 2:
            //             currentEnemyChance = chances[2];
            //             type = EnemyTypes.hard;
            //             break;
            //     }

            //     rnd = GetPooledObjectWithType(type);
            //     if(chance < currentEnemyChance)
            //     {
            //         isEnemyGot = false;
            //         return rnd;                                        
            //     }
            //     else
            //         ReturnToThePool(rnd);       
            // }
            // return null;
        }
        IEnumerator EnemySpawn(float delayBeforeSpawn) //delay should be exactly same with the particle effect duration/lifetime
        {
            yield return new WaitForSeconds(delayBeforeSpawn);
            foreach (Vector3 position in locations)
            {
                //GameObject enemy = GetPooledObjectWithType(EnemyTypes.easy);
                GameObject enemy = GetEnemy();
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
