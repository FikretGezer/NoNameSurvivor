using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class PlayerBase : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] public Transform _target;
        [SerializeField] private float _lerpSpeed = 1f;
        

        private CharacterController _characterController;
        private Camera _cam;
        
        public static PlayerBase Instance;
        public virtual void Awake() {
            if(Instance == null) Instance = this;
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
            if(Input.GetMouseButtonDown(0))
            {
                _target.gameObject.SetActive(false);
                _target = null;
            }
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
            else
            {
                if(_target.gameObject.activeInHierarchy)
                {                             
                    var pos = _target.position;
                    pos.y = transform.position.y;
                    var dir = (pos - transform.position).normalized;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), _lerpSpeed * Time.deltaTime);
                }
            }       
        }
    }
}
