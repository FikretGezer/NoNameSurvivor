using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class MoneyPoolManager : MonoBehaviour
    {
        public static List<GameObject> moneyItems = new List<GameObject>();
        public static void ClearMoneyItems()
        {
            foreach (var money in moneyItems)
            {
                Destroy(money);
            }
            moneyItems.Clear();
        }
    }
}
