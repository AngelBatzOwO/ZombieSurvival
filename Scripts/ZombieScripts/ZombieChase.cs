using UnityEngine;

public class ZombieChase : MonoBehaviour
{
    public float chaseSpeed = 2f; // Speed at which the zombie chases the player

    private Transform player;
    private ZombieDetection zombieDetection;

    private void Start()
    {
        zombieDetection = GetComponent<ZombieDetection>();
        player = zombieDetection.player;
    }

    private void Update()
    {
        if (zombieDetection.IsPlayerInRange())
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // Rotate to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
