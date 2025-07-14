using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance { get; private set; } // Singleton instance of the CoinPool

    [SerializeField] private int maxPoolSize = 50; // Maximum size of the pool
    
    public int CurrentPoolSize => pool.Count;
    public int MaxPoolSize => maxPoolSize;

    [Header("Pool Settings")] 
    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private bool autoExpand = true;

    [Header("VFX")] [SerializeField] private GameObject coinCollectVfxPrefab;

    private readonly Queue<GameObject> pool = new Queue<GameObject>(); // Queue to hold the coins in the pool

    private void Awake()
    {
        CheckInstance();
    }

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateCoin();
        }
    }

    private GameObject CreateCoin() // Creates a new coin and adds it to the pool
    {
        GameObject coin = Instantiate(coinPrefab);
        coin.SetActive(false);
        pool.Enqueue(coin);
        Debug.Log("[CoinPool] Created new coin and added to pool.");
        return coin;
    }
    
    public GameObject SpawnCoin(Vector3 position) // Spawns a coin at the specified position
    {
        if (pool.Count == 0)
        {
            if (autoExpand)
            {
                Debug.LogWarning("[CoinPool] Pool empty. Creating extra coin.");
                CreateCoin();
            }
            else
            {
                Debug.LogError("[CoinPool] Pool empty and autoExpand disabled! Cannot spawn coin.");
                return null;
            }
        }

        // Dequeue a coin from the pool and set its position
        GameObject coin = pool.Dequeue();
        coin.transform.position = position;
        coin.SetActive(true);
        Debug.Log($"[CoinPool] Spawned coin at {position}");
        return coin;
    }


    public void ReturnCoin(GameObject coin) // Returns a coin to the pool
    {
        if (coinCollectVfxPrefab != null)
        {
            Instantiate(coinCollectVfxPrefab, coin.transform.position, Quaternion.identity);
        }

        coin.SetActive(false);
        pool.Enqueue(coin);
        Debug.Log($"[CoinPool] Returned coin to pool at {coin.transform.position}");

        TrimPool(); // Check if limit is exceeded
    }



    private void CheckInstance() // Ensures that only one instance of CoinPool exists in the scene
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate CoinPool found! Destroying extra.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TrimPool();
            Debug.Log("[CoinPool] TrimPool() called up via the Backspace key.");
        }
#endif
    }
    
 
    
    public void TrimPool() // Trims the pool to ensure it does not exceed maxPoolSize
    {
        int excess = pool.Count - maxPoolSize;
        if (excess > 0)
        {
            Debug.LogWarning($"[CoinPool] Trimming {excess} extra coins from pool.");

            for (int i = 0; i < excess; i++)
            {
                GameObject coin = pool.Dequeue();
                Destroy(coin);
            }
        }



    }
}