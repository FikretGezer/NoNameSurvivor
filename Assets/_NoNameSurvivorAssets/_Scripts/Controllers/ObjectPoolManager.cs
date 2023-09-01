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
        private GameObject parentOfBullets;
        private void Awake() {
            if (Instance == null) Instance = this;

            //Create parent and name it
            parentOfBullets = new GameObject();
            string parentName = objectToPool.name;
            parentOfBullets.name = parentName + "s Parent";
        }
        private void Start() {
            BeginningInstantiater(); //Creates pooled objects in the beginning
        }
        private void BeginningInstantiater()
        {
            pooledObject = new List<GameObject>();
            for (int i = 0; i < amountToPool; i++)
            {
                Spawn();
            }
        }
        private GameObject Spawn()
        {
            GameObject tmp;
            tmp = Instantiate(objectToPool.gameObject);
            tmp.SetActive(false);
            tmp.transform.parent = parentOfBullets.transform;
            pooledObject.Add(tmp);
            return tmp;
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
            return Spawn();
            //return null;
        }
        public void ReturnToThePool(GameObject pooledObject)
        {
            pooledObject.SetActive(false);
        }
    }
}
