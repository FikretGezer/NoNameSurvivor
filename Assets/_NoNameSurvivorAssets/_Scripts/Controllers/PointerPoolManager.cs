using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class PointerPoolManager : MonoBehaviour
    {
        public static PointerPoolManager Instance;
        private List<GameObject> pointerItems = new List<GameObject>();
        private GameObject pointerParent;

        [SerializeField] private int spawnAmount = 20;
        [SerializeField] private GameObject pointerPrefab;

        private void Awake()
        {
            if(Instance == null) Instance = this;
            pointerParent = new GameObject();
            pointerParent.name = "Pointer Parent";
            SpawnPointerItems();
        }
        private void SpawnPointerItems()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                CreateNewOne(false);
            }
        }
        private GameObject CreateNewOne(bool activeness)
        {
            GameObject pointer = Instantiate(pointerPrefab);
            pointer.transform.SetParent(pointerParent.transform);
            pointerItems.Add(pointer);
            pointer.SetActive(activeness);
            return pointer;
        }
        public GameObject GetPooledPointer()
        {
            foreach (GameObject pointer in pointerItems)
            {
                if(!pointer.activeInHierarchy)
                {
                    pointer.SetActive(true);
                    return pointer;
                }
            }
            return CreateNewOne(true);
        }
        public void ReturnAllToThePool()
        {
            foreach (GameObject pointer in pointerItems)
            {
                pointer.SetActive(false);
            }
        }
    }
}
