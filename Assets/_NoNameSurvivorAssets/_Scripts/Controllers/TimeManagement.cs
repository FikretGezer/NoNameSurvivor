using System;
using System.Collections;
using UnityEngine;

namespace FikretGezer
{
    public class TimeManagement : MonoBehaviour
    {
        public Action<float> OnRoundTimeChanged = delegate{};
        public Action<int> OnLevelChanged = delegate{};
        public static TimeManagement Instance;

        [SerializeField] private float timePerRound = 10f;
        [SerializeField] private GameObject InGameMenu;
        [SerializeField] private GameObject EndOfRunMenu;

        private bool isRoundStarted;
        private int currentLevel; //This will be maxed at 10lvl
        public float currentTime;

        private void Awake() {
            if (Instance == null) Instance = this;
            currentLevel = 1;
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
        private void IncreaseTimeEachRound()
        {
            if(timePerRound < 60f)
                timePerRound += 5f;
        }
        private void UpdateCurrentLevel()
        {
            currentLevel++;
            OnLevelChanged(currentLevel);
        }
        private void EndTheRound()
        {
            Time.timeScale = 0f;
            XPPoolManager.Instance.ReturnAllToThePool();
            MoneyPoolManager.Instance.ReturnAllToThePool();
            PointerPoolManager.Instance.ReturnAllToThePool();
            EnemySpawnController.Instance.ReturnAllToThePool();                
            
            InGameMenu.SetActive(false);
            EndOfRunMenu.SetActive(true);
        }
        public void GetBackToTheGame()
        {
            IncreaseTimeEachRound();
            UpdateCurrentLevel();

            Time.timeScale = 1f;

            EndOfRunMenu.SetActive(false);
            InGameMenu.SetActive(true);
            
            isRoundStarted = false;
            HealthController.Instance.UpdateNewHealth(0);
            EnemySpawnController.Instance.StartNewWave();
        }
    }
}
