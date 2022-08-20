using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public int key;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance { get; private set; }

    public List<Pool> pools;
    public Dictionary<int, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = transform;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.key, objectPool);
        }
    }

    public GameObject SpawnFromPool(int tag, Vector3 position, Quaternion rotation)
    {
        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }

    public GameObject SpawnFromPoolOriginalRotation(int tag, Vector3 position)
    {
        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;

        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }

    public GameObject SpawnFromPoolOriginalTransform(int tag)
    {
        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }
}
