using System.Collections;
using UnityEngine;

namespace FikretGezer
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 _distanceBetweenPlayerAndCamera;
        [SerializeField] private Transform _playerBody;
        [SerializeField] private float _mouseSpeed = 100f;
        [SerializeField] private float _lerpSpeed = 2f;

        public float shakeAmount, shakeDuration; 
        public static CameraMovement Instance;
        public bool isShaking;
        private void Awake() {
            //Cursor.lockState = CursorLockMode.Locked;
            if(Instance == null) Instance = this;
            isShaking = true;
        }
        private void Update() {
            FollowPlayer();
        }
        private void FollowPlayer()
        {
            //var posPlayer = _playerBody.position;
            var posPlayer = CharacterSpawner.Instance._position;
            posPlayer.y = transform.position.y;
            posPlayer += _distanceBetweenPlayerAndCamera;

            transform.position = Vector3.Lerp(transform.position, posPlayer, _lerpSpeed * Time.deltaTime);
        }
        public void StartShake()
        {
            StartCoroutine(CameraShake()); 
        }
        public void StopShake()
        {
            StopCoroutine(CameraShake()); 
        }
        private IEnumerator CameraShake()
        {
            var elapsedTime = 0f;            
            while(elapsedTime < shakeDuration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = transform.position + Random.insideUnitSphere * shakeAmount;
                if (!isShaking)
                    break;
                yield return null;
            }
        }
    }
}
