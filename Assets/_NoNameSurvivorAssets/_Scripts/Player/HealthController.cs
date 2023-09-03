using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FikretGezer
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private float maxHealth = 10f;
        [SerializeField] private bool gotHit;
        [SerializeField] private float lerpSpeed = 1f;
        [SerializeField] private float lerpSpeedForText = 1f;
        private float currentHealth;
        private float reducedFillAmount;
        private float previousCurrentHealth;
        private void Awake() {
            healthImage.fillAmount = reducedFillAmount = 1f;
            currentHealth = previousCurrentHealth = maxHealth;
            //healthText.text = currentHealth.ToString();
            healthText.text = $"{currentHealth:0}";
        }
        private void Update() {
            
            if(gotHit)
            {
                currentHealth -= 2f;
                reducedFillAmount = currentHealth / maxHealth;
                gotHit = false;
            }
            if(healthImage.fillAmount > reducedFillAmount)
            {
                healthImage.fillAmount = Mathf.MoveTowards(healthImage.fillAmount, reducedFillAmount, lerpSpeed * Time.deltaTime);
                
            }
            if(previousCurrentHealth > currentHealth)
            {
                previousCurrentHealth = Mathf.MoveTowards(previousCurrentHealth, currentHealth, lerpSpeedForText * Time.deltaTime);
                healthText.text = $"{previousCurrentHealth:0}";
            }
            //healthImage.fillAmount = reducedHealth;
        }
    }
}
