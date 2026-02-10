using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
public class ZombieSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject zombiePrefab; // The zombie prefab to spawn
    public Transform spawnPoint; // The point where zombies will be spawned
    public float minSpawnTime = 3f; // Minimum spawn interval
    public float maxSpawnTime = 7f; // Maximum spawn interval
    private bool spawningActive = true; // Controls if spawning is active
    private void Start()
    {
        // Ensure prefab and spawn point references are assigned
        if (zombiePrefab == null || spawnPoint == null)
        {
            Debug.LogError("ZombieSpawner: Missing zombiePrefab or spawnPoint refereremce Please assign them in the Inspector.");
        return;
        }
        // Start spawning zombies
        StartCoroutine(SpawnZombieRoutine());
    }
    private IEnumerator SpawnZombieRoutine()
    {
        while (spawningActive)
        {
            // Wait for a random interval between minSpawnTime and maxSpawnTime
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);
            // Spawn the zombie at the spawn point
            SpawnZombie();
        }
    }
    private void SpawnZombie()
    {
        if (zombiePrefab != null && spawnPoint != null)
        {
            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("ZombieSpawner: SpawnZombie called but prefab or spawn point is null.Check your references.");
        }
    }
    // Optional: Stop spawning zombies
    public void StopSpawning()
    {
        spawningActive = false;
    }
    // Optional: Resume spawning zombies
    public void StartSpawning()
    {
        if (!spawningActive)
        {
            spawningActive = true;
            StartCoroutine(SpawnZombieRoutine());
        }
    }
}