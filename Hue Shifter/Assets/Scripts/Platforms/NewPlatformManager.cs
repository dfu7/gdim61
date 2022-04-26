using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Changes from Prior Manage:
 * New manager is a bit lighter, some functionality removed but
 * reacts the same currently (can be re-added if needed)
 * 
 * uses less variables & stores platforms as children of obj
 * uses self transform as spawnpoint
 */

public class NewPlatformManager : MonoBehaviour
{
   [SerializeField] private List<GameObject> platformPrefabs = new List<GameObject>();

   [SerializeField] private float platformSpawnCooldown = 3;
   private float cooldownTimer;

   [SerializeField] float maxSpawnDistanceX;
   [SerializeField] float maxSpawnDistanceY;

   void Start() {
      cooldownTimer = platformSpawnCooldown;
   }

   void Update() {
      cooldownTimer -= Time.deltaTime;

      if(cooldownTimer <= 0) {
         SpawnPlatform();
         cooldownTimer = platformSpawnCooldown;
      }
   }

   void SpawnPlatform() {

      // TODO account for size of prev platform if req

      // get position
      Vector3 platformPosition = transform.position;
      platformPosition.x += Random.Range(-maxSpawnDistanceX, maxSpawnDistanceX);
      platformPosition.y += Random.Range(-maxSpawnDistanceY, maxSpawnDistanceY);

      // TODO rotation ?
      Quaternion platformRotation = Quaternion.identity;

      // spawn object as child of this
      GameObject newPlatform = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Count)], transform);
      // set proper pos & rot
      newPlatform.transform.position = platformPosition;
      newPlatform.transform.rotation = platformRotation;
   }
}
