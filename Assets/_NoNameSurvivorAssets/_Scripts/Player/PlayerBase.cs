using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class PlayerBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] public Transform _target;
        [SerializeField] private float _lerpSpeed = 1f;
        [field:SerializeField] public float Health { get; set; }

        private CharacterController _characterController;

        [Header("Ground Parameters")]
        [SerializeField] private float gravity = -9.81f * 4f;
        [SerializeField] private Transform groundObject;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;
        
        private Vector3 velocity;
        protected Camera _cam;
        public virtual void Awake() {
            _characterController = GetComponent<CharacterController>();
            _cam = Camera.main;
        }
        
        public virtual void Update() {
            
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");

            Falling();
            Moving(hor, ver);
            TargetTheEnemy(); 
        }
        private void OnCollisionEnter(Collision other) {
            if(other.gameObject.CompareTag("xp"))
            {
                other.gameObject.SetActive(false);
                ExperienceManager.Instance.UpdateExperience(1);
            }
            if(other.gameObject.CompareTag("money"))
            {
                other.gameObject.SetActive(false);  
                CurrencyManager.Instance.UpdateCurrency(1);
            }
        }
        private void OnCollisionStay(Collision other) {
            if(other.gameObject.CompareTag("xp"))
            {
                other.gameObject.SetActive(false);                
                ExperienceManager.Instance.UpdateExperience(1);
            }
            if(other.gameObject.CompareTag("money"))
            {
                other.gameObject.SetActive(false);  
                CurrencyManager.Instance.UpdateCurrency(1);
            }
        }
        public abstract Transform ChooseTarget();
        private void Moving(float hor, float ver)
        {
            Vector3 move = new Vector3(hor, 0, ver);
            _characterController.Move(move * _moveSpeed * Time.deltaTime);
        }
        private void Falling()
        {
            bool isGrounded = Physics.CheckSphere(groundObject.position, groundDistance, groundMask);
            if(isGrounded && velocity.y < 0)
                velocity.y = -2f;

            velocity.y += gravity * Time.deltaTime;
            _characterController.Move(velocity * Time.deltaTime);
        }
        private void TargetTheEnemy()
        {            
            if(_target == null)
            {
                _target = ChooseTarget();
                Debug.Log("Selected");
            }
            else
            {              
                if(_target.gameObject.activeInHierarchy)
                {                             
                    var pos = _target.position;
                    pos.y = transform.position.y;
                    var direction = (pos - transform.position).normalized;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _lerpSpeed * Time.deltaTime);
                }
                else _target = null;
            }       
        }
        public void TakeDamage(float GivingDamage)
        {
            Health -= GivingDamage;
            HealthController.Instance.RearrangeHealth(GivingDamage);
            if(HealthController.Instance.currentHealth <= 0f) Die();
        }
        public void Die()
        {
            Time.timeScale = 0f;
            Debug.Log("<color=red> DIED! </color>");
        }
    }
}
