using System;
using UnityEngine;

namespace FikretGezer
{
    public class CurrencyManager : MonoBehaviour
    {
        public Action<int> OnCurrencyChanged = delegate{};
        public int money;
        public static CurrencyManager Instance;
        private void Awake() {
            if(Instance == null) Instance = this;
        }
        public void UpdateCurrency(int amount)
        {
            money += amount;
            OnCurrencyChanged(money);
        }
    }
}
