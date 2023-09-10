using System.Collections;
using UnityEngine;

namespace FikretGezer
{
    public class RoundController : MonoBehaviour
    {
        [SerializeField] private CharacterSpawner _characterSpawner;
        private bool isActive;
        public static RoundController Instance;
        private void Awake() {
            if(Instance == null) Instance = this;
        }
        public void NewRoundStart()
        {
            StartCoroutine(nameof(Timer));
        }
        IEnumerator Timer()
        {
            _characterSpawner.enabled = false;
            yield return new WaitForSeconds(0.2f);
            _characterSpawner.enabled = true;
        }
    }
}
