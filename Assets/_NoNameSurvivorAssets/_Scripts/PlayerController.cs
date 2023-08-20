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

        [Header("Gravity Things")]
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private Transform _groundObject;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundDistance;

        private CharacterController _characterController;
        Vector3 _velocity;

        private void Awake() {
            _characterController = GetComponent<CharacterController>();
        }
        private void Update() {
            
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            bool isGrounded = IsGrounded();
            if(isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            
            //Vector3 move = transform.right * hor + transform.forward * ver;
            Vector3 move = new Vector3(hor, 0, ver);
            _characterController.Move(move * _moveSpeed * Time.deltaTime);

            if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                float eq = _jumpHeight * _jumpSpeed * _gravity;
                _velocity.y = Mathf.Sqrt(eq);
            }

            _velocity.y += _gravity * _fallSpeed * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
        private bool IsGrounded()
        {
            return Physics.CheckSphere(_groundObject.position, _groundDistance, _groundLayer);
        }
    }
}
