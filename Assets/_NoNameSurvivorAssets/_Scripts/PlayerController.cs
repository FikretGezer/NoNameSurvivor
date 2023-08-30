using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class PlayerController : PlayerBase
    {   
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
