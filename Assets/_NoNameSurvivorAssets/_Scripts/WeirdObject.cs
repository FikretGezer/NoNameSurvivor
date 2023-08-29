using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FikretGezer
{
    public class WeirdObject : MonoBehaviour
    {
        public Action OnThisSpawned;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Transform bulletPoint;
        [SerializeField] private float _speed = 1f;
        private Renderer _renderer;
        private Camera _camera;
        private Rigidbody _rb;

        private void Awake() {
            _renderer = GetComponent<Renderer>();
            _camera = Camera.main;
            _rb = GetComponent<Rigidbody>();
        }
        private void OnEnable() {
            StartCoroutine(nameof(ObjectColorOverTime));   
        }
        private void Update() {
            if(!_camera.IsObjectVisible(_renderer))
            {
                ObjectPoolManager.Instance.ReturnToThePool(gameObject); //If object is out of camera view, returns to the pool;
            }    
            OnThisSpawned?.Invoke();
        }
        IEnumerator ObjectColorOverTime()
        {
            var elapsedTime = 0f;
            while(elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                _renderer.material.color = _gradient.Evaluate(elapsedTime);
                yield return null;
            }
        }
    }
}
