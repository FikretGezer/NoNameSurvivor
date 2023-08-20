using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;
        [SerializeField] private float _mouseSpeed = 100f;
        [SerializeField] private float _lerpSpeed = 2f;
        [SerializeField] private Vector3 _mesafe;


        [SerializeField] private LayerMask _layer;
        [SerializeField] private GameObject _object;
        private float xRotation;
        private void Awake() {
            //Cursor.lockState = CursorLockMode.Locked;
        }
        private void Update() {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSpeed * Time.deltaTime;

            var posPlayer = _playerBody.position;
            posPlayer.y = transform.position.y;
            posPlayer += _mesafe;

            transform.position = Vector3.Lerp(transform.position, posPlayer, _lerpSpeed * Time.deltaTime); 
            //_playerBody.Rotate(Vector3.up * mouseX);
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layer))
            {
                _object.transform.position = hit.point;
            }
            var lookPos = _object.transform.position;
            lookPos.y = _playerBody.transform.position.y;
            _playerBody.LookAt(lookPos);
        }
    }
}
