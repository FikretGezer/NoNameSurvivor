using System;
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
        [SerializeField] private float lerpSpeed = 1f;
        [SerializeField] private float lerpSpeedForText = 1f;
        public float currentHealth;
        private float previousCurrentHealth;
        private float reducedFillAmount;

        public static HealthController Instance;
        private void Awake() {
            if (Instance == null) Instance = this;

            healthImage.fillAmount = reducedFillAmount = 1f;
            currentHealth = previousCurrentHealth = maxHealth;
            healthText.text = $"{currentHealth:0}/{maxHealth:0}";
        }
        private void Update() {
            
            reducedFillAmount = currentHealth / maxHealth;  
            if(healthImage.fillAmount > reducedFillAmount)
            {
                healthImage.fillAmount = Mathf.MoveTowards(healthImage.fillAmount, reducedFillAmount, lerpSpeed * Time.deltaTime);  
            }
            if(previousCurrentHealth > currentHealth)
            {
                previousCurrentHealth = Mathf.MoveTowards(previousCurrentHealth, currentHealth, lerpSpeedForText * Time.deltaTime);
                healthText.text = $"{previousCurrentHealth:0}/{maxHealth:0}";
            }
        }
        public void RearrangeHealth(float damageAmount)
        {
            if(currentHealth > 0)
                currentHealth -= damageAmount;
        }
        public void UpdateNewHealth(float newMaxHealth)
        {
            if(newMaxHealth != 0)
                maxHealth = newMaxHealth;
                
            currentHealth = previousCurrentHealth = maxHealth;
            healthImage.fillAmount = 1f;
            healthText.text = $"{previousCurrentHealth:0}/{maxHealth:0}";
        }
    }
}
