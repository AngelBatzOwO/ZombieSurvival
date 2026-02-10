using UnityEngine;

public class HoverJitterEffect : MonoBehaviour
{
    // Adjustable jitter intensity and speed
    public float jitterIntensity = 0.05f;
    public float jitterSpeed = 20f;

    private Vector3 initialPosition;
    private bool isHovering = false;

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Apply jitter effect if the mouse is hovering over the object
        if (isHovering)
        {
            float jitterX = Mathf.PerlinNoise(Time.time * jitterSpeed, 0f) * jitterIntensity * 2 - jitterIntensity;
            float jitterY = Mathf.PerlinNoise(0f, Time.time * jitterSpeed) * jitterIntensity * 2 - jitterIntensity;
            transform.localPosition = initialPosition + new Vector3(jitterX, jitterY, 0);
        }
        else
        {
            // Reset position when not hovering
            transform.localPosition = initialPosition;
        }
    }

    // Detect mouse hover
    void OnMouseEnter()
    {
        isHovering = true;
    }

    // Detect when mouse is no longer hovering
    void OnMouseExit()
    {
        isHovering = false;
    }
}
