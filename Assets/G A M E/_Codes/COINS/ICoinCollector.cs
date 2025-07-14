/// <summary>
/// Interface to abstract coin collection functionality.
/// Helps decouple Coin trigger logic from Coin management.
/// </summary>
using UnityEngine;
public interface ICoinCollector
{
    void CollectCoin(Vector3 position);
}