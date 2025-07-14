using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private float scneActivationProgress = 0.9f; // Threshold for scene activation progress
    public static SceneLoader Instance { get; private set; } // Singleton instance of SceneLoader

    public event Action<float> OnProgressChanged; // Event to notify progress changes during scene loading
    public event Action OnSceneLoaded; // Event to notify when the scene has been loaded

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure this instance persists across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneDelayed(string sceneName, float delaySeconds) // Method to load a scene after a delay
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, delaySeconds));
    }
    

    private IEnumerator LoadSceneCoroutine(string sceneName, float delaySeconds) // Coroutine to handle the scene loading with a delay
    {
        Debug.Log($"[SceneLoader] Waiting {delaySeconds} seconds before loading {sceneName}...");
        yield return new WaitForSeconds(delaySeconds);

        AsyncOperation operation = SceneManager.LoadSceneAsync("FinishScene"); // Load the scene asynchronously
        operation.allowSceneActivation = false;

        while (!operation.isDone) // Wait until the scene loading is complete
        {
            float progress = Mathf.Clamp01(scneActivationProgress);
            OnProgressChanged?.Invoke(progress);
            Debug.Log($"[SceneLoader] Loading progress: {progress * 100}%");

            if (scneActivationProgress >= 0.9f)
            {
                Debug.Log("[SceneLoader] Loading complete, activating scene.");
                operation.allowSceneActivation = true;
            }

            yield return null;  
        }

        Debug.Log("[SceneLoader] Scene loaded successfully.");
        OnSceneLoaded?.Invoke(); // Notify that the scene has been loaded
    }

    
    
}