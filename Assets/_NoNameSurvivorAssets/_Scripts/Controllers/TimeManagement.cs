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
        public int CurrentLevel {get; private set;} //This will be maxed at 10lvl
        public float currentTime;
        public bool isNewRoundStarted;

        private void Awake() {
            if (Instance == null) Instance = this;
            CurrentLevel = 1;
            isNewRoundStarted = false;
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
            if(CurrentLevel < 10)
                CurrentLevel++;
            OnLevelChanged(CurrentLevel);
        }
        private void EndTheRound()
        {
            CameraMovement.Instance.StopShake();
            Time.timeScale = 0f;

            BulletEnemyPoolManager.Instance.ReturnAllToThePool(); 
            BulletPoolManager.Instance.ReturnAllToThePool(); 
            EnemySpawnController.Instance.ReturnAllToThePool(); 
            MoneyPoolManager.Instance.ReturnAllToThePool(); 
            MoneyTextPoolManager.Instance.ReturnAllToThePool();             
            MoneyPoolManager.Instance.ReturnAllToThePool();
            PointerPoolManager.Instance.ReturnAllToThePool();
            XPPoolManager.Instance.ReturnAllToThePool();
            XPTextPoolManager.Instance.ReturnAllToThePool();
                                     
            InGameMenu.SetActive(false);            
            EndOfRunMenu.SetActive(true);


            if(CurrentLevel >= 10)
            {
                //GAME FINISHED
            }
        }
        public void GetBackToTheGame()
        {
            EnemySpawnController.Instance.gotPooledObject = false;            
            isNewRoundStarted = true;
            EnemySpawnController.Instance.spawnIncreaser = CurrentLevel;
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
