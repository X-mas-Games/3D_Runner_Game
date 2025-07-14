using UnityEngine;

/// <summary>
/// Detects player collision and triggers coin collection logic,
/// then returns the coin to the pool.
/// Depends on ICoinCollector to allow flexible management.
/// </summary>
public class TriggerCoins : MonoBehaviour 
{
    [SerializeField] private MonoBehaviour coinCollectorBehaviour; 
    private ICoinCollector coinCollector;

    private void Awake()
    {
        if (coinCollectorBehaviour == null) 
        {
            Debug.LogError("[TriggerCoins] coinCollectorBehaviour is not assigned in the inspector!");
            enabled = false;
            return;
        }
        
        
        coinCollector = coinCollectorBehaviour as ICoinCollector; // Attempt to cast the MonoBehaviour to ICoinCollector
        if (coinCollector == null)
        {
            Debug.LogError("[TriggerCoins] coinCollectorBehaviour does not implement ICoinCollector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (coinCollector != null)
            {
                coinCollector.CollectCoin(transform.position);
            }

            if (CoinPool.Instance != null)
            {
                CoinPool.Instance.ReturnCoin(gameObject);
            }
            else
            {
                Debug.LogWarning("[TriggerCoins] CoinPool.Instance don't find!");
            }
        }
    }
}