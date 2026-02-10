using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the standard input system
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movement = new Vector2(horizontalInput, verticalInput);

        // Set the animator parameter based on movement
        bool isMoving = movement.magnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }

    void RotateToFaceMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle in degrees and set the rotation of the player  
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 95f);
    }
}

/* 
Purpose of the script:
- This script handles player movement in a top-down 2D environment using keyboard input.
- It updates the Animator component's "IsMoving" parameter to switch between idle and moving animations.

How to Implement:
1. Attach this script to your Player GameObject.
2. Ensure the Player GameObject has a Rigidbody2D and an Animator component.
3. Assign the Animator to the script's animator field in the Inspector.
4. Set your moveSpeed in the Inspector as desired.
5. Press Play and use WASD or Arrow keys to move, causing the character to switch animations.
*/
