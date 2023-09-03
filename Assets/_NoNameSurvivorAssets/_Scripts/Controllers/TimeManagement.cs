using System;
using System.Collections;
using UnityEngine;

namespace FikretGezer
{
    public class TimeManagement : MonoBehaviour
    {
        public Action<float> UpdateRoundTimeText = delegate{};
        [SerializeField] private float timePerRound = 10f;
        private bool isRoundStarted;
        public static TimeManagement Instance;
        private void Awake() {
            if (Instance == null) Instance = this;
        }
        private void Update() {
            if(!isRoundStarted)
            {
                StartCoroutine(nameof(RoundTimerCountDown));
                isRoundStarted = true;
            }
        }
        private IEnumerator RoundTimerCountDown()
        {
            float reducedTime = timePerRound;
            while(reducedTime > 0f)
            {
                UpdateRoundTimeText(reducedTime);
                reducedTime -= Time.deltaTime;
                yield return null;
            }
            //Time.timeScale = 0f;
            isRoundStarted = false;
        }
    }
}
