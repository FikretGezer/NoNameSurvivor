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

        [Header("Gravity Things")]
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private Transform _groundObject;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundDistance;

        private CharacterController _characterController;
        private Vector3 _velocity;
        private Camera _cam;
        
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
            RotateCharacter();
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
        private void RotateCharacter()
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
        private bool IsGrounded()
        {
            return Physics.CheckSphere(_groundObject.position, _groundDistance, _groundLayer);
        }
    }
}
