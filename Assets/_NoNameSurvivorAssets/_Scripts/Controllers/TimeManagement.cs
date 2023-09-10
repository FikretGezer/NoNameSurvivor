using System;
using System.Collections;
using UnityEngine;

namespace FikretGezer
{
    public class TimeManagement : MonoBehaviour
    {
        [SerializeField] private float timePerRound = 10f;
        [SerializeField] private GameObject InGameMenu;
        [SerializeField] private GameObject EndOfRunMenu;
        public Action<float> OnRoundTimeChanged = delegate{};
        private bool isRoundStarted;
        public static TimeManagement Instance;
        public float currentTime;
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
                OnRoundTimeChanged(reducedTime);
                reducedTime -= Time.deltaTime;
                currentTime = reducedTime;
                yield return null;
            }
            EndTheRound();
        }
        private void EndTheRound()
        {
            Time.timeScale = 0f;
            InGameMenu.SetActive(false);
            EndOfRunMenu.SetActive(true);
            EnemySpawnController.Instance.ReturnAll();                
        }
        public void GetBackToTheGame()
        {
            Time.timeScale = 1f;
            EndOfRunMenu.SetActive(false);
            InGameMenu.SetActive(true);
            isRoundStarted = false;
            HealthController.Instance.UpdateNewHealth(0);
        }
    }
}
