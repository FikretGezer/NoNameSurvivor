using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class PlayerController : PlayerBase
    {
        // public override Transform ChooseTarget()
        // {
        //     Transform _target;
        //     var selectionIndex = Random.Range(0, EnemySpawnController.Instance.enemies.Count);
        //     _target = EnemySpawnController.Instance.enemies[selectionIndex].transform;
        //     if(!EnemySpawnController.Instance.selectedEnemies.Contains(_target.gameObject))
        //     {
        //         EnemySpawnController.Instance.selectedEnemies.Add(EnemySpawnController.Instance.enemies[selectionIndex]);
        //         return _target;
        //     }
        //     return null;
        // }     
        public override Transform ChooseTarget()
        {
            Transform _target = null;
            float minDist = 100000f;
            foreach(var trgt in EnemySpawnController.Instance.enemies)
            {
                if(!EnemySpawnController.Instance.selectedEnemies.Contains(trgt.gameObject))
                {
                    var dist = (trgt.transform.position - transform.position).sqrMagnitude;
                    if(dist < minDist)
                    {
                        minDist = dist;
                        _target = trgt.transform;
                    }
                }                
            }
            if(_target != null)
                EnemySpawnController.Instance.selectedEnemies.Add(_target.gameObject);
            return _target;
        }  
    }
}
