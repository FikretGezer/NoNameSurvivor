using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class RoundEnder : MonoBehaviour
    {
        [SerializeField] private CharacterSpawner _characterSpawner;
        private bool isActive;
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
