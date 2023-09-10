using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FikretGezer
{
    public class UiUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text roundTimeText;
        [SerializeField] private TMP_Text currencyText;
        [SerializeField] private TMP_Text experienceText;

        [SerializeField] private Image _actualExperience;

        [SerializeField] private TimeManagement _timeManagement;
        [SerializeField] private ExperienceManager _experienceManager;
        [SerializeField] private CurrencyManager _currencyManager;
        
        private float expFillAmount;
        private void OnEnable() {
            _timeManagement.OnRoundTimeChanged += UpdateRoundTimer;
            _experienceManager.OnExperienceChanged += UpdateExperienceUI;
            _currencyManager.OnCurrencyChanged += UpdateCurrencyUI;
        }
        private void OnDisable() {
            _timeManagement.OnRoundTimeChanged -= UpdateRoundTimer;
            _experienceManager.OnExperienceChanged -= UpdateExperienceUI;
            _currencyManager.OnCurrencyChanged -= UpdateCurrencyUI;
        }

        public void UpdateRoundTimer(float currentTime)
        {
            roundTimeText.text = $"{currentTime:0}";
        }
        private void UpdateExperienceUI(int currentExperience, int maxExperience, int currentLevel)
        {
            experienceText.text = $"Lv.{currentLevel:0}";

            expFillAmount = (float)currentExperience / maxExperience;
            _actualExperience.fillAmount = expFillAmount;           
        }
        private void UpdateCurrencyUI(int currency)
        {
            currencyText.text = $"{currency:0}";
        }
    }
}
