using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FikretGezer
{
    public class MenuAnimController : MonoBehaviour
    {
        [SerializeField] private TMP_Text die;        
        [SerializeField] private TMP_Text xp;        
        [SerializeField] private TMP_Text xpAmount;        
        [SerializeField] private TMP_Text money;        
        [SerializeField] private TMP_Text moneyAmount;

        [SerializeField] private GameObject _buttonPlayAgain;
        [SerializeField] private GameObject _buttonHome;

        [SerializeField] private float timeSlower = 0.01f;
        public void SetDieTextAlpha()
        {
            StartCoroutine(SetAlpha(timeSlower, die));
        }
        public void SetMoneyTextAlpha()
        {
            StartCoroutine(SetAlpha(timeSlower, money));
        }
        public void SetMoneyAmountTextAlpha()
        {
            moneyAmount.text = CurrencyManager.Instance.totalMoney.ToString();
            StartCoroutine(SetAlpha(timeSlower, moneyAmount));
        }
        public void SetXpTextAlpha()
        {
            StartCoroutine(SetAlpha(timeSlower, xp));
        }
        public void SetXpAmountTextAlpha()
        {
            xpAmount.text = ExperienceManager.Instance.totalExperience.ToString();
            StartCoroutine(SetAlpha(timeSlower, xpAmount));
        }
        public void SetActivePlayButton()
        {
            _buttonPlayAgain.SetActive(true);
        }
        public void SetActiveHomeButton()
        {
            _buttonHome.SetActive(true);
        }
        IEnumerator SetAlpha(float f, TMP_Text t)
        {
            float val = 0;
            while(val < 1f)
            {
                val += Time.time * f;
                t.color = new Color(1, 1, 1, val);
                yield return null;
            }
        }

    }
}
