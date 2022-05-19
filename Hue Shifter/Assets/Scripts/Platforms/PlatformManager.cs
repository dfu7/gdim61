using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] List<GameObject> platformPrefabs = new List<GameObject>();
    private List<GameObject> activePlatforms = new List<GameObject>();

    [SerializeField] Transform currentSpawnpoint;

    public float platformSpawnCooldown = 3;
    private bool canSpawn = true;
    private float cooldownTimer;

    public float maxSpawnDistanceX;
    public float maxSpawnDistanceY;

    GameObject currentPlatform;
    [SerializeField] Transform playerTrans;

    void Start()
    {
        cooldownTimer = platformSpawnCooldown;
        currentPlatform = platformPrefabs[Random.Range(0, platformPrefabs.Count)];
    }

    void Update()
    {
        if (canSpawn)
        {
            GenerateSpawnpoint();
            activePlatforms.Add(Instantiate(currentPlatform, currentSpawnpoint));
            //activePlatforms.Add(Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Count - 1)], spawnpoint2));
            canSpawn = false;
        }
        else
        {
            if (cooldownTimer <= 0)
            {
                canSpawn = true;
                cooldownTimer = platformSpawnCooldown;
            }
            else
            {
                cooldownTimer -= Time.deltaTime;
            }
        }
    }


    void GenerateSpawnpoint()
    {
        //size of previous platform
        Vector3 lastPlatformSize = currentPlatform.GetComponentInChildren<Renderer>().bounds.size;
        float minSpawnX1 = lastPlatformSize.x / 2;
        float minSpawnY1 = lastPlatformSize.y / 2;

        //new platform
        currentPlatform = platformPrefabs[Random.Range(0, platformPrefabs.Count)];

        //size of new platform
        Vector3 currentPlatformSize = currentPlatform.GetComponentInChildren<Renderer>().bounds.size;
        float minSpawnX2 = currentPlatformSize.x / 2;
        float minSpawnY2 = currentPlatformSize.y / 2;

        //random x & y distance with a min of prev platform size + new platform size and a set max
        float randomXDist = Random.Range(currentSpawnpoint.position.x + minSpawnX2 + minSpawnX1 - maxSpawnDistanceX/2, maxSpawnDistanceX/2);
        float randomYDist = Random.Range(currentSpawnpoint.position.y + minSpawnY2 + minSpawnY1, maxSpawnDistanceY);

       currentPlatform.transform.position = new Vector3(randomXDist, randomYDist, 50 + playerTrans.position.z);
    }
}
