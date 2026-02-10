using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject player;
    public PlayerHealth playerHealthScript;

    [Header("Audio Clips")]
    public AudioClip failSFX;
    public AudioClip diedMusic;
    public AudioClip winMusic;

    [Header("Game Controls")]
    public GameObject restartButton;
    public GameObject quitButton;

    [Header("UI Elements")]
    public TextMeshProUGUI youLoseText; // TextMeshPro UI element for "You Lose" message
    public TextMeshProUGUI youWinText;  // TextMeshPro UI element for "You Win" message

    [Header("Adjustable Timer Settings")]
    public float pauseDelay = 2.0f; // Adjustable number of seconds to wait before pausing the game

    private AudioSource audioSource;
    private AudioSource musicAudioSource; // Dedicated AudioSource for music
    private bool isGameOver = false;

    void Start()
    {
        // Initialize UI and buttons
        restartButton.SetActive(false);
        quitButton.SetActive(false);

        if (youLoseText != null)
        {
            youLoseText.gameObject.SetActive(false);
        }
        if (youWinText != null)
        {
            youWinText.gameObject.SetActive(false);
        }

        // Initialize AudioSources
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        musicAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (isGameOver) return;

        // Example win condition logic (adjust this to match your game)
        if (WinConditionMet())
        {
            PlayerWon();
        }
    }

    public void OnPlayerDeath()
    {
        if (isGameOver) return;

        isGameOver = true;
        StartCoroutine(HandlePlayerDeath());
    }

    IEnumerator HandlePlayerDeath()
    {
        // Play Fail SFX
        if (failSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(failSFX);
            yield return new WaitForSecondsRealtime(failSFX.length);
        }

        // Play Died Music
        if (diedMusic != null && musicAudioSource != null)
        {
            musicAudioSource.clip = diedMusic;
            musicAudioSource.Play();
        }

        // Show Lose Text
        if (youLoseText != null)
        {
            youLoseText.gameObject.SetActive(true);
        }

        // Wait before activating buttons and pausing
        yield return new WaitForSecondsRealtime(pauseDelay);

        // Activate Restart and Quit buttons
        if (restartButton != null) restartButton.SetActive(true);
        if (quitButton != null) quitButton.SetActive(true);

        // Pause the game
        Time.timeScale = 0;
    }

    void PlayerWon()
    {
        isGameOver = true;

        // Play the WinMusic
        if (winMusic != null && audioSource != null)
        {
            audioSource.clip = winMusic;
            audioSource.Play();
        }

        // Show the "You Win" message
        if (youWinText != null)
        {
            youWinText.gameObject.SetActive(true);
        }

        // Start coroutine to pause game after a delay
        StartCoroutine(PauseGameAfterDelay());
    }

    bool WinConditionMet()
    {
        // Placeholder for win condition logic
        return false;
    }

    IEnumerator PauseGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(pauseDelay);

        if (restartButton != null) restartButton.SetActive(true);
        if (quitButton != null) quitButton.SetActive(true);

        Time.timeScale = 0;
    }
}