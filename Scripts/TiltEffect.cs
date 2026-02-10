using UnityEngine;

public class TiltEffect : MonoBehaviour
{
    // Adjustable tilt angle and speed
    public float tiltAngle = 30f;
    public float tiltSpeed = 2f;

    private float time;

    void Update()
    {
        // Increment time to animate the tilt
        time += Time.deltaTime * tiltSpeed;

        // Calculate the eased tilt angle using Mathf.SmoothStep
        float angle = Mathf.SmoothStep(-tiltAngle, tiltAngle, Mathf.PingPong(time, 1f));

        // Apply the rotation around the Z-axis
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
