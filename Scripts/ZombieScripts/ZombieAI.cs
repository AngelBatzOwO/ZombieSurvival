using System.Collections;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float patrolSpeed = 1f;
    public float chaseSpeed = 3f;
    public float detectionRange = 10f;
    public float walkDistanceMin = 0.5f;
    public float walkDistanceMax = 2f;
    public float walkTimeMin = 0.5f;
    public float walkTimeMax = 2f;

    [Header("Return to Center Settings")]
    public float maxOffScreenTime = 3f;
    private Vector3 screenCenter;
    private float offScreenTimer = 0f;

    [Header("References")]
    public LayerMask obstacleLayer;
    private Rigidbody2D rb;
    private GameObject player;

    [Header("Animator")]
    public Animator animator; // Reference to the Animator controlling ZombieWalk

    private bool isChasing = false;
    private bool isReturningToCenter = false;
    private Vector2 movement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        screenCenter = Vector3.zero; // Center of the game screen
        StartCoroutine(PatrolRoutine());
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            isChasing = distanceToPlayer <= detectionRange;
        }

        // Check if off-screen and manage return to center
        if (!IsVisibleFrom(GetComponent<Renderer>(), Camera.main))
        {
            offScreenTimer += Time.deltaTime;
            if (offScreenTimer >= maxOffScreenTime)
            {
                StartCoroutine(ReturnToCenterRoutine());
            }
        }
        else
        {
            offScreenTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (isChasing && player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            movement = direction * chaseSpeed;
            MoveZombie(movement);
        }
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!isChasing && !isReturningToCenter)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                float walkDistance = Random.Range(walkDistanceMin, walkDistanceMax);
                float patrolDuration = walkDistance / patrolSpeed;

                float elapsedTime = 0f;
                while (elapsedTime < patrolDuration && !isChasing)
                {
                    movement = randomDirection * patrolSpeed;
                    MoveZombie(movement);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // After finishing movement, the zombie stops walking
                animator.SetBool("IsWalking", false);

                // Wait for a random time before moving again
                yield return new WaitForSeconds(Random.Range(walkTimeMin, walkTimeMax));
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator ReturnToCenterRoutine()
    {
        isReturningToCenter = true;

        while (Vector3.Distance(transform.position, screenCenter) > 0.1f)
        {
            Vector2 directionToCenter = (screenCenter - transform.position).normalized;
            movement = directionToCenter * patrolSpeed;
            MoveZombie(movement);

            yield return null;
        }

        // After reaching center, zombie stops walking
        animator.SetBool("IsWalking", false);

        isReturningToCenter = false;
    }

    private void MoveZombie(Vector2 movement)
    {
        // Check for obstacles before moving
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.normalized, movement.magnitude * Time.fixedDeltaTime, obstacleLayer);
        if (hit.collider == null && movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

            // Rotate to face movement direction
            RotateZombie(movement);

            // Zombie is moving, play walk animation
            animator.SetBool("IsWalking", true);
        }
        else
        {
            // No movement due to obstacle or zero vector
            animator.SetBool("IsWalking", false);
        }
    }

    private void RotateZombie(Vector2 movement)
    {
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f;
        }
    }

    private bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}

/*
Purpose of the Script:
The script manages a zombie character that patrols, chases the player, and returns to the screen center if off-screen 
too long. While the zombie is moving (patrolling, chasing, or returning to center), the "ZombieWalk" animation plays.

How to Implement:
1. Attach this script to the zombie GameObject.
2. Assign the Animator in the Inspector and ensure it has a bool parameter "IsWalking" that triggers the "ZombieWalk" animation.
3. Adjust values like patrolSpeed, chaseSpeed, detectionRange, and off-screen timing as needed.
4. Ensure the zombie and player have the appropriate tags and colliders.
5. The zombie will now patrol and chase, and the walking animation will play whenever it is moving.
*/