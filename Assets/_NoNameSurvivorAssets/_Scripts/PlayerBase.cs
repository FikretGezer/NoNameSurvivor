using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class PlayerBase : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private Transform _target;
        

        private CharacterController _characterController;
        private Camera _cam;
        

        public virtual void Awake() {
            _characterController = GetComponent<CharacterController>();
            _cam = Camera.main;
        }
        public virtual void Start() {
            //_target = GameObject.FindGameObjectWithTag("target").transform;
        }
        
        public virtual void Update() {
            
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");

            Controls();
            Moving(hor, ver);
            //RotateCharacterWithMouse();
            RotateCharacterToEnemy();
        }
        private void Controls()
        {
        }
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
        public abstract Transform ChooseTarget();
        private void RotateCharacterToEnemy()
        {            
            if(_target == null)
            {
                _target = ChooseTarget();
            }
            if(_target.gameObject.activeInHierarchy)
            {                             
                var pos = _target.position;
                pos.y = transform.position.y;
                transform.LookAt(pos);
            }
        }
    }
}
