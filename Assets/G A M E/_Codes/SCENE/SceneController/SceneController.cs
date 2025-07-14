using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void FinishedScene()
    {
        SceneManager.LoadScene("FinishScene");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}