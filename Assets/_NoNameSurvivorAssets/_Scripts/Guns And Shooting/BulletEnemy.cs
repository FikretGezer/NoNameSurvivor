using System;
using System.Collections;
using UnityEngine;

namespace FikretGezer
{
    public class BulletEnemy : MonoBehaviour
    {
        private Action OnShoot = delegate{};
        [SerializeField] private Gradient _gradient;

        private Renderer _renderer;
        private Camera _camera;
        private float damage;
        private bool isDisappearCountdownStarted;
        public Vector3 direction;
        public float bulletSpeed;
        private void Awake() {
            _renderer = GetComponent<Renderer>();
            _camera = Camera.main;
        }
        private void OnEnable() {
            StartCoroutine(nameof(ObjectColorOverTime)); 
            isDisappearCountdownStarted = true;  
            direction = Vector3.zero;
        }
        private void Update() {
            // if(!_camera.IsObjectVisible(_renderer))
            // {
            //     direction = Vector3.zero;
            //     gameObject.SetActive(false); //If object is out of camera view, returns to the pool;
            // } 
            if(isDisappearCountdownStarted)
            {
                Debug.Log("damn");
                StartCoroutine(nameof(DissapearTimer));                
            }

            if(direction != Vector3.zero) MoveObject();

           // OnShoot.Invoke();
        }
        public void MoveObject()
        {
            transform.Translate(direction * bulletSpeed * Time.deltaTime);
        }
        public void ShootThis(Action shoot)
        {
            OnShoot = shoot;
        }
        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player"))
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
        public float elapsedTime;
        IEnumerator DissapearTimer()
        {
            //var elapsedTime = 0f;
            elapsedTime = 0f;
            isDisappearCountdownStarted = false;
            while (elapsedTime < 5f)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
