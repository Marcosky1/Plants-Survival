using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public int poolSize = 10;
    public float spawnRate = 2f;
    public Transform[] spawnPoints;
    private List<GameObject> zombiePool;

    void Start()
    {
        zombiePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(zombiePrefab);
            obj.SetActive(false);
            zombiePool.Add(obj);
        }

        InvokeRepeating(nameof(SpawnZombie), spawnRate, spawnRate);
    }

    void SpawnZombie()
    {
        foreach (GameObject zombie in zombiePool)
        {
            if (!zombie.activeInHierarchy)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                zombie.transform.position = spawnPoints[spawnPointIndex].position;
                zombie.SetActive(true);
                break;
            }
        }
    }
}

