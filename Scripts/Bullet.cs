using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f; // Lifetime of the bullet in seconds

    void Start()
    {
        // Destroy the bullet after a set lifetime
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy bullet if it collides with a "Zombie" or "Obstacle"-tagged object
        if (other.CompareTag("Zombie") || other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // Destroy the bullet if it leaves the screen
        Destroy(gameObject);
    }
}

/*
Purpose of the script:
This script manages the bullet's lifetime and behavior upon collisions.
It automatically destroys the bullet after a certain lifetime, 
when it collides with "Zombie" or "Obstacle"-tagged objects, 
or when it leaves the camera's view.

How to Implement:
1. Attach this script to the Bullet prefab.
2. Make sure the Bullet object has a Collider2D with "Is Trigger" enabled.
3. Ensure the objects you want the bullet to destroy on impact 
   have the tags "Zombie" or "Obstacle".
4. Adjust the lifetime as desired in the Inspector.
*/