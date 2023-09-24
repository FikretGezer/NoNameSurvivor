using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class PoolManagersBase : MonoBehaviour
    {
        protected List<GameObject> items;
        private GameObject objectsParent;
        
        [Header("Pool Parameters")]
        //[field: SerializeField] protected GameObject objectPrefab;
        [field: SerializeField] protected List<GameObject> objectPrefabs;
        [field: SerializeField] protected string objectParentsName;
        [field: SerializeField] protected int poolItemSpawnAmount;
        [field: SerializeField] protected GameObject itemsParent;

        public virtual void Awake()
        {
            items = new List<GameObject>();
            if(itemsParent == null)
                objectsParent = new GameObject();   
            else
                objectsParent = itemsParent;         
            objectsParent.name = objectParentsName;
            SpawnItems();
        }
        private void SpawnItems()
        {
            for (int prefabCount = 0; prefabCount < objectPrefabs.Count; prefabCount++)
            {
                GameObject selectedPrefab = objectPrefabs[prefabCount];
                for (int i = 0; i < poolItemSpawnAmount; i++)
                {
                    CreateNewOne(false, selectedPrefab);
                }                
            }
        }
        private GameObject CreateNewOne(bool activeness, GameObject selectedPrefab)
        {            
            GameObject item = Instantiate(selectedPrefab);
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
            GameObject randomObject = objectPrefabs[Random.Range(0, objectPrefabs.Count)];
            return CreateNewOne(true, randomObject);
        }
        //This is just for the enemy spawner
        public GameObject GetPooledObjectWithType(EnemyTypes type)
        {
            foreach (GameObject item in items)
            {
                if(!item.activeInHierarchy && item.GetComponent<EnemyBase>()._enemyType == type)
                {
                    item.SetActive(true);
                    return item;
                }
            }
            GameObject randomObject = objectPrefabs[Random.Range(0, objectPrefabs.Count)];
            return CreateNewOne(true, randomObject);
        }
        public void ReturnToThePool(GameObject item)
        {
            item.SetActive(false);
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
