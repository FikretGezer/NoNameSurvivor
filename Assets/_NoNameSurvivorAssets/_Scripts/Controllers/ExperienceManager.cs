using System;
using UnityEngine;

namespace FikretGezer
{
    public class ExperienceManager : MonoBehaviour
    {
        public Action<int,int,int> OnExperienceChanged = delegate{};
        public Func<bool> OnLeveledUp;
        public static ExperienceManager Instance;

        // public int CurrentExperience {get; private set;}
        // public int MaxExperience {get; private set;}
        [SerializeField] private int currentExperience;
        [SerializeField] private int maxExperience;
        private int currentXPLevel;
        public int totalExperience;

        private void Awake() {
            if(Instance == null) Instance = this;

            currentXPLevel = 1;
            currentExperience = 0;
            maxExperience = 10;
            UpdateExperience(0);
        }
        
        public void UpdateExperience(int amount)
        {
            currentExperience += amount;
            totalExperience += amount;
            IsLeveledUp();
            OnExperienceChanged(currentExperience, maxExperience, currentXPLevel);
        }
        private void IsLeveledUp()
        {
            if(currentExperience >= maxExperience)
            {
                currentExperience = currentExperience - maxExperience;
                currentXPLevel++;
                int increasedXP = maxExperience * 20 / 100;
                maxExperience += increasedXP;
            }
        }
    }
}
