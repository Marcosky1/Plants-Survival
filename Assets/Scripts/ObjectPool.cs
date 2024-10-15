using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab; 
    public int poolSize = 10; 
    private Queue<GameObject> poolQueue;

    private void Start()
    {
        poolQueue = new Queue<GameObject>();


        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); 
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject GetFromPool()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true); 
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(objectPrefab);
            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj); 
    }
}

