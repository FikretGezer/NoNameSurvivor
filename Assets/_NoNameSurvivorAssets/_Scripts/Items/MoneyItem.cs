using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class MoneyItem : MonoBehaviour
    {
        [SerializeField] private Transform _groundObject;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _radius = 0.1f;
        [HideInInspector] public int moneyAmount = 1;

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
                CurrencyManager.Instance.UpdateCurrency(moneyAmount);
                gameObject.SetActive(false);
            }            
        }
    }
}