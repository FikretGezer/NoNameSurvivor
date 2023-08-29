using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance;
        private List<GameObject> pooledObject;
        [SerializeField] private WeirdObject objectToPool;
        [SerializeField] private int amountToPool;
        private void Awake() {
            if (Instance == null) Instance = this;
        }
        private void Start() {
            BeginningInstantiater(); //Creates pooled objects in the beginning
        }
        private void BeginningInstantiater()
        {
            pooledObject = new List<GameObject>();
            GameObject tmp;
            string parentName = objectToPool.name;
            GameObject parent = new GameObject();
            parent.name = parentName + "s Parent";
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool.gameObject);
                tmp.SetActive(false);
                tmp.transform.parent = parent.transform;
                pooledObject.Add(tmp);
            }
        }
        public GameObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if(!pooledObject[i].activeInHierarchy)
                {
                    return pooledObject[i];
                }
            }
            return null;
        }
        public void ReturnToThePool(GameObject pooledObject)
        {
            pooledObject.SetActive(false);
        }
    }
}
