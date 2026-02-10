using System.Collections;
using UnityEngine;

public class ZombieWalk : MonoBehaviour
{
    private float walkSpeed;
    private float walkDistance;
    private float walkTime;
    private Vector3 walkDirection;
    private bool isWalking = false;
    private bool isReturningToCenter = false;
    private Vector3 screenCenter;
    private float offScreenTime = 0f;
    private float maxOffScreenTime = 3f;

    private void Start()
    {
        screenCenter = Vector3.zero; // Assuming the center of the game screen is at (0, 0)
        StartCoroutine(StartWalking());
    }

    private void Update()
    {
        if (!isReturningToCenter && IsOffScreen())
        {
            offScreenTime += Time.deltaTime;
            if (offScreenTime >= maxOffScreenTime)
            {
                StartCoroutine(ReturnToCenter());
            }
        }
        else
        {
            offScreenTime = 0f;
        }
    }

    private IEnumerator StartWalking()
    {
        while (true)
        {
            if (!isWalking && !isReturningToCenter)
            {
                walkSpeed = Random.Range(0.5f, 2f);
                walkDistance = Random.Range(0.5f, 2f);
                walkTime = Random.Range(0.5f, 2f);
                walkDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

                StartCoroutine(Walk());
            }

            yield return new WaitForSeconds(walkTime);
        }
    }

    private IEnumerator Walk()
    {
        isWalking = true;
        float distanceTraveled = 0f;

        while (distanceTraveled < walkDistance)
        {
            Vector3 movement = walkDirection * walkSpeed * Time.deltaTime;
            transform.position += movement;
            distanceTraveled += movement.magnitude;

            // Rotate to face the walk direction
            float angle = Mathf.Atan2(walkDirection.y, walkDirection.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            yield return null;
        }

        isWalking = false;
    }

    private IEnumerator ReturnToCenter()
    {
        isReturningToCenter = true;

        while (Vector3.Distance(transform.position, screenCenter) > 0.1f)
        {
            Vector3 directionToCenter = (screenCenter - transform.position).normalized;
            Vector3 movement = directionToCenter * walkSpeed * Time.deltaTime;
            transform.position += movement;

            // Rotate to face the center direction
            float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            yield return null;
        }

        isReturningToCenter = false;
    }

    private bool IsOffScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;
    }
}

// Purpose of the script: The script allows the zombie character to walk randomly, and if it goes off-screen for more than 3 seconds, it returns to the center of the game screen before resuming random movement.
// How to Implement: Attach this script to the zombie GameObject in Unity. The zombie will randomly walk, face its movement direction, and return to the center of the screen if off-screen for too long. Ensure the main camera is set up correctly to determine screen bounds.
