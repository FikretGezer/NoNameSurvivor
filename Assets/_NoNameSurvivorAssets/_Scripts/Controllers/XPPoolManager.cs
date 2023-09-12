using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class XPPoolManager : MonoBehaviour
    {
        public static XPPoolManager Instance;
        private List<GameObject> xpItems = new List<GameObject>();
        private GameObject xpParent;

        [SerializeField] private int spawnAmount = 20;
        [SerializeField] private GameObject xpPrefab;

        private void Awake()
        {
            if(Instance == null) Instance = this;
            xpParent = new GameObject();
            xpParent.name = "XP Parent";
            SpawnXPItems();
        }
        private void SpawnXPItems()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                CreateNewOne(false);
            }
        }
        private GameObject CreateNewOne(bool activeness)
        {
            GameObject xp = Instantiate(xpPrefab);
            xp.transform.SetParent(xpParent.transform);
            xpItems.Add(xp);
            xp.SetActive(activeness);
            return xp;
        }
        public GameObject GetPooledXP()
        {
            foreach (GameObject xp in xpItems)
            {
                if(!xp.activeInHierarchy)
                {
                    xp.SetActive(true);
                    return xp;
                }
            }
            return CreateNewOne(true);
        }
        public void ReturnAllToThePool()
        {
            foreach (GameObject xp in xpItems)
            {
                xp.SetActive(false);
            }
        }
    }
}
