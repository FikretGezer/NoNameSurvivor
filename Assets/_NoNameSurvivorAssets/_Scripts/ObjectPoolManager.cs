using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FikretGezer
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 pos)
        {
            PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

            if(pool == null)
            {
                pool = new PooledObjectInfo() { LookupString = objectToSpawn.name};
                ObjectPools.Add(pool);
            }

            GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

            if(spawnableObj == null)
            {
                spawnableObj = Instantiate(objectToSpawn);
                spawnableObj.transform.position = pos + Random.insideUnitSphere * 10f;
            }
            else
            {
                spawnableObj.transform.position = pos + Random.insideUnitSphere * 10f;
                pool.InactiveObjects.Remove(spawnableObj);
                spawnableObj.SetActive(true);
            }
            return spawnableObj;
        }
        public static void ReturnObjectToPool(GameObject obj)
        {
            string goName = obj.name.Substring(0, obj.name.Length - 7);
            PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);
            if(pool != null)
            {
                obj.SetActive(false);
                pool.InactiveObjects.Add(obj);
            }
        }
    }
    public class PooledObjectInfo
    {
        public string LookupString;
        public List<GameObject> InactiveObjects = new List<GameObject>();
    }
}
