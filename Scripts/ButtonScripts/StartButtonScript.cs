using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string gameSceneName = "Game"; // The name of the game scene to load

    public void OnStartButtonPressed()
    {
        // Load the main game scene
        SceneManager.LoadScene(gameSceneName);
    }
}
