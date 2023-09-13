using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FikretGezer
{
    public abstract class PoolManagersBase : MonoBehaviour
    {
        private List<GameObject> items;
        private GameObject objectsParent;
        
        [field: SerializeField] protected GameObject objectPrefab;
        [field: SerializeField] protected string objectParentsName;
        [field: SerializeField] protected int spawnAmount;

        public virtual void Awake()
        {
            items = new List<GameObject>();
            objectsParent = new GameObject();
            objectsParent.name = objectParentsName;
            SpawnItems();
        }
        private void SpawnItems()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                CreateNewOne(false);
            }
        }
        private GameObject CreateNewOne(bool activeness)
        {
            GameObject item = Instantiate(objectPrefab);
            item.transform.SetParent(objectsParent.transform);
            items.Add(item);
            item.SetActive(activeness);
            return item;
        }
        public GameObject GetPooledObject()
        {
            foreach (GameObject item in items)
            {
                if(!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    return item;
                }
            }
            return CreateNewOne(true);
        }
        public void ReturnAllToThePool()
        {
            foreach (GameObject item in items)
            {
                item.SetActive(false);
            }
        }
        public List<GameObject> GetListOfObjects()
        {
            return items;
        }
    }
}
