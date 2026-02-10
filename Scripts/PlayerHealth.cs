using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Damage Feedback Settings")]
    public AudioSource gruntSound;
    public AudioSource deathScreamSound;
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.05f;

    [Header("UI Settings")]
    public TMP_Text healthText;

    [Header("GameManager Reference")]
    public GameManager gameManager;

    [Header("Animation and Movement")]
    public Animator animator;          // Reference to the Animator with the Death animation
    public MonoBehaviour movementScript; // Reference to the player's movement script

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (gruntSound != null) gruntSound.Play();
        StartCoroutine(Flash());

        if (currentHealth <= 0) Die();
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < 6; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration);
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        // Trigger the death animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Stop all player movement
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        if (deathScreamSound != null)
        {
            deathScreamSound.Play();
        }

        if (gameManager != null)
        {
            gameManager.OnPlayerDeath();
        }

        StartCoroutine(DestroyPlayerAfterDeathScream());
    }

    IEnumerator DestroyPlayerAfterDeathScream()
    {
        if (deathScreamSound != null && deathScreamSound.clip != null)
        {
            yield return new WaitForSeconds(deathScreamSound.clip.length);
        }

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Destroy(gameObject);
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}";
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Zombie"))
        {
            TakeDamage(2);
        }
        else if (collision.collider.CompareTag("FastZombie"))
        {
            TakeDamage(5);
        }
        else if (collision.collider.CompareTag("StrongZombie"))
        {
            TakeDamage(7);
        }
    }
}

/*
Purpose of the Script:
This script manages player health, damage feedback, death animations, and stopping player movement upon death.

How to Implement:
1. Attach this script to the player GameObject.
2. Assign the Animator with a "Die" trigger parameter to the animator field.
3. Assign the player's movement script to the movementScript field.
4. Set the player's maxHealth, AudioSources, and UI text in the Inspector.
5. On player death, the "Die" animation will play, and movement will be disabled.
*/