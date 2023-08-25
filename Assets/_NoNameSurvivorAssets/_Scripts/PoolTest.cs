using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FikretGezer
{
    public class PoolTest : MonoBehaviour
    {
        [SerializeField] private float sizeOfRadius = 3f;
        [SerializeField] private Transform bulletPoint;
        [SerializeField] private Transform target;
        [SerializeField] private Transform cube;
        [SerializeField] private float _speed = 1f;
        private void Start() {
            //InvokeRepeating(nameof(Spawn), 0.2f, 1f);
        }
        private void Spawn()
        {
            GameObject obj = ObjectPoolManager.Instance.GetPooledObject();
            if(obj != null)
            {
                obj.transform.position = bulletPoint.position;//transform.position + Random.insideUnitSphere * sizeOfRadius;
                obj.gameObject.SetActive(true);
                obj.GetComponent<WeirdObject>().OnThisSpawned = Deneme;
            }
            void Deneme() 
            {
                var dir = (target.position - bulletPoint.position).normalized;
                dir.y = 0f;
                obj.GetComponent<Rigidbody>().AddForce(dir * _speed);
            }
        }
        private void AddSpeed()
        {
            var dir = (cube.position - bulletPoint.position).normalized;
            //obj.GetComponent<Rigidbody>().AddForce(dir * _speed);

        }
    }
}
