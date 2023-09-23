using System;
using System.Collections;
using UnityEngine;

namespace FikretGezer
{
    public class Bullet : MonoBehaviour
    {
        private Action OnShoot = delegate{};
        [SerializeField] private Gradient _gradient;

        private Renderer _renderer;
        private Camera _camera;
        private float damage;
        private bool isDisappearCountdownStarted;
        private void Awake() {
            _renderer = GetComponent<Renderer>();
            _camera = Camera.main;
        }
        private void OnEnable() {
            StartCoroutine(nameof(ObjectColorOverTime)); 
            isDisappearCountdownStarted = true;  
        }
        private void Update() {
            // if(!_camera.IsObjectVisible(_renderer))
            // {
            //     gameObject.SetActive(false); //If object is out of camera view, returns to the pool;
            // }  
            if(isDisappearCountdownStarted)
            {
                StartCoroutine(nameof(DissapearTimer));
                isDisappearCountdownStarted = false;
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
                gameObject.SetActive(false);
                other.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
        public void SetDamage(float damage)
        {
            this.damage = damage;
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
        IEnumerator DissapearTimer()//Bullet dissapear after some amount of time, if it don't hit any enemy
        {
            var elapsedTime = 0f;
            while (elapsedTime < 5f)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
