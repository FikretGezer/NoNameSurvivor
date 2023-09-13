using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public interface IPool
    {
        public static IPool Instance;
        public List<GameObject> items {get; set;}
        public GameObject objectsParent {get; set;}

        public int spawnAmount {get; set;}
        public GameObject objectPrefab {get; set;}
        public string nameOfParent {get; set;}

        public void SpawnItems();
        public GameObject CreateNewOne(bool activeness);
        public GameObject GetPooledObject();
        public void ReturnAllToThePool();
        public void AllDes();
    }
}
