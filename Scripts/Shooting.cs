using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;       // Bullet prefab to instantiate
    public Transform firePoint;           // Position and rotation where the bullet spawns
    public float moveSpeed = 10f;         // Speed of the bullet
    public float shootTiming = 0.5f;      // Minimum time between shots
    public AudioSource gunfireSound;      // Gunfire audio source

    private float lastShotTime = 0f;      // Time when the last shot was fired

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + shootTiming)
        {
            lastShotTime = Time.time;
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        // Instantiate bullet and set its position and rotation to match the firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Ensure the bullet is on the correct layer (e.g., Bullet layer)
        bullet.layer = LayerMask.NameToLayer("Bullet");

        // Set the bullet's velocity to move away from the firePoint at the specified moveSpeed
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.up * moveSpeed; // Assuming bullet moves along the X+ axis
        }

        // Play gunfire sound if available
        if (gunfireSound != null)
        {
            gunfireSound.Play();
        }
    }
}