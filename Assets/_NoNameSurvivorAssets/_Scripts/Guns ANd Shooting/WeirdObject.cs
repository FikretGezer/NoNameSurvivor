using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class WeirdObject : MonoBehaviour
    {
        private Action OnShoot = delegate{};
        [SerializeField] private Gradient _gradient;

        private Renderer _renderer;
        private Camera _camera;
        
        private void Awake() {
            _renderer = GetComponent<Renderer>();
            _camera = Camera.main;
        }
        private void OnEnable() {
            StartCoroutine(nameof(ObjectColorOverTime));   
        }
        private void Update() {
            if(!_camera.IsObjectVisible(_renderer))
            {
                ObjectPoolManager.Instance.ReturnToThePool(gameObject); //If object is out of camera view, returns to the pool;
            }  
            OnShoot.Invoke();
        }
        public void ShootThis(Action shoot)
        {
            OnShoot = shoot;
        }
        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "enemy")
            {
                ObjectPoolManager.Instance.ReturnToThePool(gameObject);
                other.GetComponent<IDamageable>().TakeDamage(1f);
            }
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
