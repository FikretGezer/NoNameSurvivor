using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class PlayerController : PlayerBase
    {
        private List<GameObject> enemyList = new List<GameObject>();
        public override void Awake() {
            base.Awake();   
        }
        private void OnEnable()
        {
            enemyList = EnemySpawnController.Instance.GetListOfObjects();
            enemyList.AddRange(MediumEnemyPoolManager.Instance.GetListOfObjects());
            enemyList.AddRange(HardEnemyPoolManager.Instance.GetListOfObjects());
        }
        public override Transform ChooseTarget()
        {
            Transform _target = null;
            float minDist = 100000f;
            //foreach(var trgt in EnemySpawnController.Instance.enemies)
            foreach(var trgt in enemyList/*EnemySpawnController.Instance.GetListOfObjects()*/)
            {
                if(trgt.activeInHierarchy && !EnemySpawnController.Instance.selectedEnemies.Contains(trgt.gameObject))
                {
                    var dist = (trgt.transform.position - transform.position).sqrMagnitude;
                    if(dist < minDist)
                    {
                        minDist = dist;
                        _target = trgt.transform;
                    }
                }
                
                // if(!EnemySpawnController.Instance.selectedEnemies.Contains(trgt.gameObject))
                // {
                //     var dist = (trgt.transform.position - transform.position).sqrMagnitude;
                //     if(dist < minDist)
                //     {
                //         minDist = dist;
                //         _target = trgt.transform;
                //     }
                // }                
            }
            if(_target != null)
                EnemySpawnController.Instance.selectedEnemies.Add(_target.gameObject);
            return _target;
        }         
    }
}
