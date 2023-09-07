using System;
using UnityEngine;

namespace FikretGezer
{
    public class CurrencyManager : MonoBehaviour
    {
        public Action<int> OnCurrencyChanged = delegate{};
    }
}
