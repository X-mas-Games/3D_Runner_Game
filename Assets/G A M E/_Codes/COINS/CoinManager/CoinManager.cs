using UnityEngine;

/// <summary>
/// Manages coin collection logic, modifies CoinData.
/// Implements ICoinCollector to respect Dependency Inversion Principle.
/// </summary>

public class CoinManager : MonoBehaviour, ICoinCollector
{
    [SerializeField] private CoinData coinData;

    private void Awake()
    {
        if (coinData == null)
        {
            Debug.LogError("[CoinManager] CoinData is not assigned in the inspector!");
            enabled = false;
            return;
        }

        coinData.LoadCoins();
    }

  
    public void CollectCoin(Vector3 position)
    {
        coinData.AddCoins(1);
        Debug.Log($"[CoinManager] Coin collected at {position}. Total coins: {coinData.CoinCount}");
    }

    
    public void ResetAllCoins()
    {
        coinData.ResetCoins();
        Debug.Log("[CoinManager] Coins reset to zero");
    }
}