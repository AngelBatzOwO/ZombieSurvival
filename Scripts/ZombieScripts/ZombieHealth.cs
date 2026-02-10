using System.Collections;
using UnityEngine;
public class ZombieHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 20;
    private int currentHealth;
    [Header("Damage Flash Settings")]
    public Renderer zombieRenderer;
    public Color damageColor = Color.red;
    public float flashDuration = 0.1f;
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip zombieDeathSound;
    private Color originalColor;
    private bool isFlashing = false;
    private bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
        if (zombieRenderer != null)
            originalColor = zombieRenderer.material.color;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Knife":
                TakeDamage(1);
                break;
            case "Bullet":
                TakeDamage(2);
                break;
        case "ShotgunShell":
                TakeDamage(3);
                break;
            case "Bullets":
                TakeDamage(15);
                break;
            case "Explosive":
                TakeDamage(7);
                break;
        }
    }
    void TakeDamage(int damage)
    {
        if (isDead) return; // Prevent taking damage after death
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashDamage());
        }
    }
    void Die()
    {
        if (isDead) return;
        isDead = true;
        // Play the ZombieDeath sound and flash twice before destruction
        StartCoroutine(DeathSequence());
    }
    IEnumerator DeathSequence()
    {
        // Play death sound
        if (audioSource != null && zombieDeathSound != null)
        {
            audioSource.PlayOneShot(zombieDeathSound);
        }
        // Flash twice before being destroyed
        for (int i = 0; i < 2; i++)
        {
            if (zombieRenderer != null)
            {
                zombieRenderer.material.color = damageColor;
                yield return new WaitForSeconds(flashDuration);
                zombieRenderer.material.color = originalColor;
                yield return new WaitForSeconds(flashDuration);
            }
        }
        // Destroy the zombie game object after flashing
        Destroy(gameObject);
    }
    IEnumerator FlashDamage()
    {
        if (isFlashing || zombieRenderer == null)
            yield break;
        isFlashing = true;
        for (int i = 0; i < 2; i++)
        {
            zombieRenderer.material.color = damageColor;
            yield return new WaitForSeconds(flashDuration);
            zombieRenderer.material.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
        isFlashing = false;
    }
}