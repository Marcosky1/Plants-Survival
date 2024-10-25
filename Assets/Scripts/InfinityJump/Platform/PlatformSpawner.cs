using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float platformSpawnDistance = 2.5f;
    public int numberOfPlatforms = 10;
    private float lastY;

    public Transform playerTransform; 

    void Start()
    {
        lastY = transform.position.y;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (playerTransform != null && playerTransform.position.y > lastY - (platformSpawnDistance * numberOfPlatforms))
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-2f, 2f), lastY + platformSpawnDistance, 0);
        Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        lastY += platformSpawnDistance; 
    }
}




