using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void OnQuitButtonPressed()
    {
        // Quit the application
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
