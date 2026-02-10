using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public Animator animator; // Reference to Animator with "Zombie Attack" animation

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger the zombie attack animation
            animator.SetTrigger("isAttacking");
        }
        else
        {
            animator.ResetTrigger("isAttacking"); animator.SetTrigger("isWalking");
        }
    }
}

/*
Purpose of the Script:
This script detects when the attached zombie GameObject collides with a GameObject tagged "Player." 
Upon collision, it plays the referenced "Zombie Attack" animation by setting an Animator trigger.

How to Implement:
1. Attach this script to the zombie GameObject.
2. Assign the Animator component that controls the "Zombie Attack" animation to the `animator` field in the Inspector.
3. In the Animator, ensure there is a trigger parameter named "Attack" that transitions into the "Zombie Attack" animation.
4. Make sure the player GameObject is tagged "Player" and has appropriate colliders set to collide with the zombie.
*/