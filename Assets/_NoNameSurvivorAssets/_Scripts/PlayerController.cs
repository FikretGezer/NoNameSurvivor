using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _jumpHeight = 2f;
        [SerializeField] private float _jumpSpeed = -2f;
        [SerializeField] private float _fallSpeed = 2f;
        [SerializeField] private Transform _object;
        [SerializeField] private Transform _target;

        [Header("Gravity Things")]
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private Transform _groundObject;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundDistance;

        private CharacterController _characterController;
        private Vector3 _velocity;
        private Camera _cam;
        private const float TAU = 6.28318530718f;
        
        private bool _isGrounded;
        private bool _isJumping;

        private void Awake() {
            _characterController = GetComponent<CharacterController>();
            _cam = Camera.main;
        }
        private void Update() {
            
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            _isGrounded = IsGrounded();

            Controls();
            Moving(hor, ver);
            //RotateCharacterWithMouse();
            RotateCharacterToEnemy();
            Jumping();
        }
        private void Controls()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
            } 
        }
        private void Jumping()
        {
            if(_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            if(_isJumping && _isGrounded)
            {
                float eq = _jumpHeight * _jumpSpeed * _gravity;
                _velocity.y = Mathf.Sqrt(eq);
                _isJumping = false;
            }

            _velocity.y += _gravity * _fallSpeed * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
        private void Moving(float hor, float ver)
        {
            Vector3 move = new Vector3(hor, 0, ver);
            _characterController.Move(move * _moveSpeed * Time.deltaTime);
        }
        private void RotateCharacterWithMouse()
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                _object.transform.position = hit.point;
            }
            var pos = _object.transform.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);
        }
        private void RotateCharacterToEnemy()
        {
            var dir = (_target.position - transform.position).normalized;
            
            if(_target.gameObject.activeInHierarchy)
            {
                var rot = Quaternion.LookRotation(dir);
                var rotY = rot.eulerAngles.y;

                var limitRot = Mathf.Rad2Deg * TAU; // 360 degrees total
                limitRot = limitRot / 2; // Every player can rotate inside of their 180 degrees area
                var perPlayer = limitRot / 2; // We got 0 as a center and player can rotate between -perPlayer & perPlayer which means for this -90f & 90f
                                              

                if(rotY > 180f) 
                {
                   rotY -= 360f;
                }
                Debug.Log(rotY);
                if(rotY >= -perPlayer && rotY <= perPlayer)
                {
                    var pos = _target.position;
                    pos.y = transform.position.y;
                    transform.LookAt(pos);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), 5f * Time.deltaTime);
                }
            }
        }
        private bool IsGrounded()
        {
            return Physics.CheckSphere(_groundObject.position, _groundDistance, _groundLayer);
        }
    }
}
