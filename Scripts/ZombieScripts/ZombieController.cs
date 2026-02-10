using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour
{
    public float detectionRange = 10f;
    public float chaseSpeed = 3f;
    public float patrolSpeed = 1f;
    public LayerMask obstacleLayer;

    public GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isChasing = false;
    private float offScreenTimer = 0f;
    private float maxOffScreenTime = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PatrolRoutine());
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= detectionRange)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }
        }

        // Destroy zombie if off-screen for too long
        if (!IsVisibleFrom(GetComponent<Renderer>(), Camera.main))
        {
            offScreenTimer += Time.deltaTime;
            if (offScreenTimer >= maxOffScreenTime)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            offScreenTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (isChasing && player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            movement = direction * chaseSpeed;
            MoveZombie(movement);
        }
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!isChasing)
            {
                // Patrol in a random direction for a random distance
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                float randomDistance = Random.Range(1f, 5f);
                Vector2 targetPosition = (Vector2)transform.position + randomDirection * randomDistance;

                float elapsedTime = 0f;
                float patrolTime = randomDistance / patrolSpeed;
                while (elapsedTime < patrolTime && !isChasing)
                {
                    movement = randomDirection * patrolSpeed;
                    MoveZombie(movement);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Wait for a random time before moving again
                float waitTime = Random.Range(1f, 3f);
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield return null;
            }
        }
    }

    void MoveZombie(Vector2 movement)
    {
        // Check for obstacles before moving
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.normalized, movement.magnitude * Time.fixedDeltaTime, obstacleLayer);
        if (hit.collider == null)
        {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

            // Rotate to face movement direction
            if (movement != Vector2.zero)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
            }
        }
    }

    bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}