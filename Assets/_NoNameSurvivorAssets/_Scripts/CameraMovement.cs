using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 _distanceBetweenPlayerAndCamera;
        [SerializeField] private Transform _playerBody;
        [SerializeField] private float _mouseSpeed = 100f;
        [SerializeField] private float _lerpSpeed = 2f;
        private void Awake() {
            //Cursor.lockState = CursorLockMode.Locked;
        }
        private void Update() {
            FollowPlayer();            
        }
        private void FollowPlayer()
        {
            var posPlayer = _playerBody.position;
            posPlayer.y = transform.position.y;
            posPlayer += _distanceBetweenPlayerAndCamera;

            transform.position = Vector3.Lerp(transform.position, posPlayer, _lerpSpeed * Time.deltaTime); 
        }
    }
}
