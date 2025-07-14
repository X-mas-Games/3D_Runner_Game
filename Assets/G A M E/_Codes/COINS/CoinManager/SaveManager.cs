using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Save the number of coins
    public void SaveCoins(int amount)
    {
        PlayerPrefs.SetInt("coinCount", amount);
        PlayerPrefs.Save();
    }

    // Load the number of coins
    public int LoadCoins()
    {
        return PlayerPrefs.GetInt("coinCount", 0);
    }
    
    public void SaveLevel(int level) // Save the current level
    {
        PlayerPrefs.SetInt("currentLevel", level);
        PlayerPrefs.Save();
    }

   
    public int LoadLevel() // Load the current level
    {
        return PlayerPrefs.GetInt("currentLevel", 1);
    }

    // Clear all saves
    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }
}