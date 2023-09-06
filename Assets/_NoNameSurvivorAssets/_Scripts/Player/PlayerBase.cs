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
        protected Camera _cam;
        
        
        public virtual void Awake() {
            _characterController = GetComponent<CharacterController>();
            _cam = Camera.main;
        }
        
        public virtual void Update() {
            
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");

            Moving(hor, ver);
            TargetTheEnemy();            
            //RotateCharacterWithMouse();
        }
        public abstract Transform ChooseTarget();
        private void Moving(float hor, float ver)
        {
            Vector3 move = new Vector3(hor, 0, ver);
            _characterController.Move(move * _moveSpeed * Time.deltaTime);
        }
        /*private void RotateCharacterWithMouse()
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                _object.transform.position = hit.point; // _object is invisible object on the scene to allow player control the characters
            }
            var pos = _object.transform.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);
        }*/
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
                    //transform.LookAt(pos);
                    var direction = (pos - transform.position).normalized;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _lerpSpeed * Time.deltaTime);
                }
                else
                {
                    _target = null;
                }
            }       
        }
        public void TakeDamage(float GivingDamage)
        {
            Health -= GivingDamage;
            HealthController.Instance.RearrangeHealth(GivingDamage);
            if(HealthController.Instance.currentHealth <= 0f)
                Die();
        }
        public void Die()
        {
            Time.timeScale = 0f;
            Debug.Log("<color=red> DIED! </color>");
        }
    }
}
