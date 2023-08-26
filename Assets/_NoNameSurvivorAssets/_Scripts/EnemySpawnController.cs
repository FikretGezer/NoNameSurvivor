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
        public List<GameObject> enemies = new List<GameObject>();
        public List<GameObject> selectedEnemies = new List<GameObject>();

        private void Awake() {
            if(Instance == null) Instance = this;
        }
        private void Start() {
            for (int i = 0; i < _spawnCount; i++)
            {
                float x = Random.Range(-6f, 6f);
                float z = Random.Range(-6f, 6f);
                var _enemy = Instantiate(_enemyPrefab, new Vector3(x, 1f, z), Quaternion.identity);
                //_enemy.tag = "enemy";
                enemies.Add(_enemy);
            }
        }
    }
}