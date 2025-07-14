using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints = new(); // List of spawn points for coins

    private void Start()
    {
        if (CoinPool.Instance == null)
        {
            Debug.LogError("[CoinSpawner] CoinPool.Instance not found! Make sure the CoinPool object is in the scene.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("[CoinSpawner] Spawn point list is empty or not assigned. Please assign spawn points in the inspector.");
            return;
        }

        SpawnCoins();
    }

   
    private void SpawnCoins() // Spawns coins at the specified spawn points
    {
        if (CoinPool.Instance == null)
        {
            Debug.LogError("[CoinSpawner] CoinPool.Instance not found. Make sure the CoinPool exists in the scene.");
            return;
        }

        foreach (var spawnPoint in spawnPoints) 
        {
            if (spawnPoint != null && spawnPoint.activeInHierarchy)
            {
                Vector3 spawnPosition = spawnPoint.transform.position; // Get the position of the spawn point
                GameObject coin = CoinPool.Instance.SpawnCoin(spawnPosition);

                if (coin != null)
                {
                    Debug.Log($"[CoinSpawner] Coin spawned at {spawnPosition}");
                }
            }
        }
    }
    

    private bool IsValidSpawnPoint(GameObject point) // Checks if the spawn point is valid (not null and active in hierarchy)
    {
        return point != null && point.activeInHierarchy;
    }
}