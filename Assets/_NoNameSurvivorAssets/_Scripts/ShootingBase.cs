using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class ShootingBase : MonoBehaviour
    {
        [SerializeField] protected Transform bulletPoint;
        [SerializeField] protected Transform target;
        [SerializeField] protected float speed;

        public virtual void Start() {
            ChooseTarget();
            InvokeRepeating(nameof(Spawn), 0.2f, 1f);
        }
        public virtual void Update()
        {
            ChooseTarget();
        }
        private void ChooseTarget()
        {
            if(target == null)
            {
                target = PlayerBase.Instance._target;
            }
        }
        private void Spawn()
        {
            GameObject obj = ObjectPoolManager.Instance.GetPooledObject();
            if(target != null)
            {
                if(obj != null)
                {
                    obj.transform.position = bulletPoint.position;//transform.position + Random.insideUnitSphere * sizeOfRadius;
                    obj.gameObject.SetActive(true);
                    obj.GetComponent<WeirdObject>().OnThisSpawned = ShootBullet;
                }
                void ShootBullet() 
                {
                    var dir = (target.position - bulletPoint.position).normalized;
                    dir.y = 0f;
                    obj.transform.Translate(dir * speed * Time.deltaTime);
                }
            }
            
        }
           
    }
}
