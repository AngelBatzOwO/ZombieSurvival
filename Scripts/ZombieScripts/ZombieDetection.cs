using UnityEngine;

public class ZombieDetection : MonoBehaviour
{
    public float detectionRange = 5f; // Detection range to spot the player
    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool IsPlayerInRange()
    {
        if (player == null) return false;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }
}
