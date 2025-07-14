using NUnit.Framework;
using UnityEngine;

public class CoinDataTests
{
    private CoinData coinData;

    [SetUp]
    public void Setup() 
    {
        coinData = ScriptableObject.CreateInstance<CoinData>();
        coinData.ResetCoins();
    }

    [Test]
    public void AddCoins_AddsCorrectAmount() 
    {
        coinData.AddCoins(5);
        Assert.AreEqual(5, coinData.CoinCount);
    }

    [Test]
    public void AddCoins_DoesNotAddIfNegative()
    {
        coinData.AddCoins(-2);
        Assert.AreEqual(0, coinData.CoinCount);
    }

    [Test]
    public void ResetCoins_SetsToZero() 
    {
        coinData.AddCoins(10);
        coinData.ResetCoins();
        Assert.AreEqual(0, coinData.CoinCount);
    }
}