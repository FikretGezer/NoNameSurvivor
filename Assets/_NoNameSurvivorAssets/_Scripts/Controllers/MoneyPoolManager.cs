using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class MoneyPoolManager : MonoBehaviour
    {
        public static MoneyPoolManager Instance;
        private List<GameObject> moneyItems = new List<GameObject>();
        private GameObject moneyParent;

        [SerializeField] private int spawnAmount = 20;
        [SerializeField] private GameObject moneyPrefab;

        private void Awake()
        {
            if(Instance == null) Instance = this;
            moneyParent = new GameObject();
            moneyParent.name = "Money Parent";
            SpawnMoneyItems();
        }
        private void SpawnMoneyItems()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                CreateNewOne(false);
            }
        }
        private GameObject CreateNewOne(bool activeness)
        {
            GameObject money = Instantiate(moneyPrefab);
            money.transform.SetParent(moneyParent.transform);
            moneyItems.Add(money);
            money.SetActive(activeness);
            return money;
        }
        public GameObject GetPooledMoney()
        {
            foreach (GameObject money in moneyItems)
            {
                if(!money.activeInHierarchy)
                {
                    money.SetActive(true);
                    return money;
                }
            }
            return CreateNewOne(true);
        }
        public void ReturnAllToThePool()
        {
            foreach (GameObject money in moneyItems)
            {
                money.SetActive(false);
            }
        }
    }
}
