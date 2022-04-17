using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] List<GameObject> platformPrefabs = new List<GameObject>();
    private List<GameObject> activePlatforms = new List<GameObject>();

    [SerializeField] Transform spawnpoint1;
    [SerializeField] Transform spawnpoint2;

    public float platformDespawnZ = -20;

    public float platformSpawnCooldown = 3;
    private bool canSpawn = true;
    private float cooldownTimer;

    void Start()
    {
        cooldownTimer = platformSpawnCooldown;
    }

    void Update()
    {
        if (canSpawn)
        {
            activePlatforms.Add(Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Count-1)], spawnpoint1));
            activePlatforms.Add(Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Count - 1)], spawnpoint2));
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

        CheckDespawn();
    }

    void CheckDespawn()
    {
        foreach (var x in activePlatforms)
        {
            if (x.transform.position.z <= platformDespawnZ)
            {
                Destroy(x);
            }
        }        
    }
}
