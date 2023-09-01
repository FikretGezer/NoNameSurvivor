using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FikretGezer
{
    public class UiUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text roundTimeText;
        [SerializeField] private TimeManagement _timeManagement;
        private void OnEnable() {
            _timeManagement.UpdateRoundTimeText += UpdateRoundTimer;
        }
        
        private void OnDisable() {
            _timeManagement.UpdateRoundTimeText -= UpdateRoundTimer;
        }
        public void UpdateRoundTimer(float currentTime)
        {
            roundTimeText.text = $"{currentTime:0}";
        }
    }
}
