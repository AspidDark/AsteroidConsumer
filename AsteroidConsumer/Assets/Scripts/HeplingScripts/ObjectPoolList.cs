﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;


namespace TimB
{
    [System.Serializable]
    public class PooledPrefabs
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public class ObjectPoolList : MonoBehaviour
    {
        public static ObjectPoolList instance;
        public Dictionary<string, List<GameObject>> pooledListDictionary;
        public List<PooledPrefabs> pools;

        private void Awake()
        {
            instance = instance ?? this;

            pooledListDictionary = new Dictionary<string, List<GameObject>>();
            foreach (var pool in pools)
            {
                List<GameObject> objectPool = new List<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Add(obj);
                }
                //if (string.IsNullOrEmpty(pool.tag))
                //{
                //    poolName = pool.prefab.name;
                //}
                //else
                //{
                //    poolName = pool.tag;
                //}
                // pooledListDictionary.Add(pool.tag, objectPool);
                string poolName = string.IsNullOrEmpty(pool.tag) ? pool.prefab.name : pool.tag;
                pooledListDictionary.Add(poolName, objectPool);
            }
        }
        // Use this for initialization
        void Start()
        {
          
        }
        /// <summary>
        /// Get Object from pool
        /// </summary>
        /// <param name="tag">name of an oblect</param>
        /// <param name="position">position to set object at</param>
        /// <param name="rotation">rotation to set object at</param>
        /// <param name="canGrow">will object pool grow?</param>
        /// <returns></returns>
        public GameObject GetPooledObject(string tag, Vector3 position, Quaternion rotation, bool canGrow = false, bool activate = true)
        {
            if (!pooledListDictionary.ContainsKey(tag))
            {
                return null;
            }
            foreach (var item in pooledListDictionary[tag])
            {
                if (!item.activeInHierarchy)
                {
                    GameObject objectToSpawn = item;
                    objectToSpawn.transform.position = position;
                    objectToSpawn.transform.rotation = rotation;
                    objectToSpawn.SetActive(activate);
                    return objectToSpawn;
                }
            }
            if (canGrow)
            {
                GameObject objectToSpawn = Instantiate(pooledListDictionary[tag].First());
                pooledListDictionary[tag].Add(objectToSpawn);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
                objectToSpawn.SetActive(activate);
                return objectToSpawn;
            }
            return null;
        }
        public GameObject GetPooledObjectWithData(string tag, Vector3 position, Quaternion rotation, BaseDTO data, bool canGrow = false, bool activate = true)
        {
            GameObject go = GetPooledObject(tag, position, rotation, canGrow, activate);
            go.GetComponent<DataReciverBase>().ReceiveData(data);

            return go;
        }



        public GameObject GeneratePositionedObjectWithData(string tag, BaseDTO data, Vector3 position, Quaternion quaternion, bool activate = true)
        {
            GameObject go = GeneratePositionedObject(tag, position, quaternion, activate);
            go.GetComponent<DataReciverBase>().ReceiveData(data);
            return go;
        }

        public GameObject GeneratePositionedObject(string tag, Vector3 position, Quaternion quaternion, bool activate = true)
        {
            if (!pooledListDictionary.ContainsKey(tag))
            {
                return null;
            }
            foreach (var item in pooledListDictionary[tag])
            {
                GameObject objectToSpawn = item;
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = quaternion;
                objectToSpawn.SetActive(activate);
                return objectToSpawn;
            }
            return null;
        }
    }
}
