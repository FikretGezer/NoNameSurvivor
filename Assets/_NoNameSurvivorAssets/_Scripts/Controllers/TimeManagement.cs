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
        [SerializeField] private GameObject dieMenu;
        [SerializeField] private GameObject winMenu;

        private bool isRoundStarted;
        public int CurrentLevel {get; private set;} //This will be maxed at 10lvl
        public float currentTime;
        public bool isNewRoundStarted;
        public bool refuelHealth;
        public bool isGameEnded;
        private bool enableDeathMenu;
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
            if(isGameEnded)
            {
                enableDeathMenu = true;
                EndTheRound();
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
            
            Time.timeScale = 0f;

            BulletEnemyPoolManager.Instance.ReturnAllToThePool(); 
            BulletPoolManager.Instance.ReturnAllToThePool(); 
            EnemySpawnController.Instance.ReturnAllToThePool(); 
            MediumEnemyPoolManager.Instance.ReturnAllToThePool(); 
            HardEnemyPoolManager.Instance.ReturnAllToThePool(); 
            MoneyPoolManager.Instance.ReturnAllToThePool(); 
            MoneyTextPoolManager.Instance.ReturnAllToThePool();             
            MoneyPoolManager.Instance.ReturnAllToThePool();
            PointerPoolManager.Instance.ReturnAllToThePool();
            XPPoolManager.Instance.ReturnAllToThePool();
            XPTextPoolManager.Instance.ReturnAllToThePool();
            CameraMovement.Instance.isShaking = false;

            InGameMenu.SetActive(false);            

            if(enableDeathMenu)
            {
                isGameEnded = false;
                Time.timeScale = 1f;
                dieMenu.SetActive(true);                
            }
            else
            {
                if(CurrentLevel >= 10)
                {
                    //Won The Game
                    winMenu.SetActive(true);
                }            
                else
                {
                    ItemSelection.Instance.EnableCards();
                    EndOfRunMenu.SetActive(true);
                }
            }
        }
        public void GetBackToTheGame()
        {
            EnemySpawnController.Instance.gotPooledObject = false;            
            isNewRoundStarted = true;
            refuelHealth = true;
            EnemySpawnController.Instance.spawnIncreaser = CurrentLevel;
            HealthController.Instance.SetHealthBack();
            IncreaseTimeEachRound();
            UpdateCurrentLevel();

            Time.timeScale = 1f;
            CameraMovement.Instance.isShaking = true;

            EndOfRunMenu.SetActive(false);
            InGameMenu.SetActive(true);
            
            isRoundStarted = false;
            HealthController.Instance.UpdateNewHealth(0);
            EnemySpawnController.Instance.StartNewWave();
            ItemsHolder.Instance.RemoveSelectedItem();
        }
    }
}
