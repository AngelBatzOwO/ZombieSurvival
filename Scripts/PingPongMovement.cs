using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    // Adjustable distance and speed
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    // Store the initial position of the object
    private Vector3 initialPosition;

    void Start()
    {
        // Set the initial position at the start
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the ping-pong movement
        float movement = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;

        // Apply the movement along the x-axis
        transform.position = initialPosition + new Vector3(movement, 0, 0);
    }
}
