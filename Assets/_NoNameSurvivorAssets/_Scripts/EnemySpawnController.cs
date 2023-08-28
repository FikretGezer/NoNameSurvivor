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
        public List<GameObject> enemies = new List<GameObject>();
        public List<GameObject> selectedEnemies = new List<GameObject>();

        private void Awake() {
            if(Instance == null) Instance = this;
        }
        private void Start() {
            for (int i = 0; i < _spawnCount; i++)
            {
                float x = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                float z = Random.Range(-_sizeOfSpawnArea, _sizeOfSpawnArea);
                var _enemy = Instantiate(_enemyPrefab, new Vector3(x, 1f, z), Quaternion.identity);
                //_enemy.tag = "enemy";
                enemies.Add(_enemy);
            }
        }
    }
}
