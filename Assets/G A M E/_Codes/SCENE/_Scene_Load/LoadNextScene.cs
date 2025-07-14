using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneNameToLoad = "FinishScene";
    [SerializeField] private float timerSceneLoad;

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;

            // SceneLoader to delay and load the scene
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadSceneDelayed(sceneNameToLoad, timerSceneLoad);
            }
            
        }
    }
}
