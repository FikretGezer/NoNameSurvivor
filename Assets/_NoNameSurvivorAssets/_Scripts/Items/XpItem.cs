using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class XpItem : MonoBehaviour
    {
        public Transform _groundObject;
        public LayerMask _layer;
        public float _radius = 0.1f;

        private Rigidbody _rb;
        private void Awake() {
            _rb = GetComponent<Rigidbody>();               
        }
        private void OnEnable() {
            _rb.velocity = Vector3.zero;    
            _rb.constraints = RigidbodyConstraints.None;        
        }
        private void Update() {
            bool isOnTheFloor = Physics.CheckSphere(_groundObject.position, _radius, _layer);     

            if(isOnTheFloor)
            {
                _rb.constraints = RigidbodyConstraints.FreezePositionY;
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                ExperienceManager.Instance.UpdateExperience(1);
                gameObject.SetActive(false);
            }            
        }
    }
}
