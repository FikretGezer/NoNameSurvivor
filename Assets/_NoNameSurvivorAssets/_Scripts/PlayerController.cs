using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class PlayerController : PlayerBase
    {
        public override void Awake()
        {
            base.Awake();
        }
        public override void Start()
        {
            base.Start();
        }
        public override void Update()
        {
            base.Update();
        }

        public override Transform ChooseTarget()
        {
            Transform _target;
            var selectionIndex = Random.Range(0, EnemySpawnController.Instance.enemies.Count);
            _target = EnemySpawnController.Instance.enemies[selectionIndex].transform;
            if(!EnemySpawnController.Instance.selectedEnemies.Contains(_target.gameObject))
            {
                EnemySpawnController.Instance.selectedEnemies.Add(EnemySpawnController.Instance.enemies[selectionIndex]);
                return _target;
            }
            return null;
        }     
    }
}
