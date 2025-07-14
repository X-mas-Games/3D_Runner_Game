using TMPro;
using UnityEngine;

/// <summary>
/// Updates the UI Text element to show current coin count.
/// Subscribes to CoinData.OnCoinsChanged for reactive updates.
/// </summary>
public class SceneLoaderUI : MonoBehaviour
{
    [SerializeField] private CoinData coinData;
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        if (coinData == null)
        {
            Debug.LogError("[SceneLoaderUI] CoinData dont assing!");
            return;
        }

        if (coinText == null)
        {
            Debug.LogError("[SceneLoaderUI] Coin Text (TextMeshProUGUI) dont assing!");
            return;
        }

        coinData.OnCoinsChanged += UpdateCoinText;
        UpdateCoinText(coinData.CoinCount);
    }

    private void OnDisable()
    {
        coinData.OnCoinsChanged -= UpdateCoinText;
    }

    /// <summary>
    /// Updates the coin text UI with a prefix.
    /// </summary>
    private void UpdateCoinText(int count)
    {
        coinText.text = $"Coins: {count}";
    }
}