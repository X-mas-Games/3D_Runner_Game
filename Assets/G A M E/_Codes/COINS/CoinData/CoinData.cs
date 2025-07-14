using System;
using UnityEngine;


[CreateAssetMenu(fileName = "CoinData", menuName = "Game/Coin Data")]
public class CoinData : ScriptableObject
{
    [SerializeField] public int coinCount;
    public int CoinCount => coinCount;

    public event Action<int> OnCoinsChanged;

    
    public void AddCoins(int amount) // Adds coins to the current coin count
    {
        if (amount <= 0) return;

        coinCount += amount;
        OnCoinsChanged?.Invoke(coinCount);
        SaveCoins();
    }

    
    public void ResetCoins() // Resets the coin count to zero
    {
        coinCount = 0;
        OnCoinsChanged?.Invoke(coinCount);
        SaveCoins();
    }

    public void LoadCoins() // Loads the coin count from the SaveManager
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogError("[CoinData] SaveManager.Instance not found!");
            coinCount = 0;
        }
        else
        {
            coinCount = SaveManager.Instance.LoadCoins();
        }
        Debug.Log($"[CoinData] Loaded coin count: {coinCount}"); // Log the loaded coin count
        OnCoinsChanged?.Invoke(coinCount); // Notify listeners about the loaded coin count
    }

    private void SaveCoins() // Saves the current coin count using the SaveManager
    {
        if (!SaveManager.Instance)
        {
            Debug.LogError("[CoinData] SaveManager.Instance not found!");
            return;
        }
        SaveManager.Instance.SaveCoins(coinCount); // Save the current coin count
    }
}