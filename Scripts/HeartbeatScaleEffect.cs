using UnityEngine;

public class HeartbeatScaleEffect : MonoBehaviour
{
    // Adjustable scale sizes for the heartbeat effect
    public Vector3 minScale = new Vector3(1f, 1f, 1f);
    public Vector3 maxScale = new Vector3(1.2f, 1.2f, 1.2f);

    // Adjustable heartbeat speeds
    public float normalHeartbeatSpeed = 1f;

    public float currentHeartbeatSpeed;
    private float heartbeatTime;
    private Vector3 initialScale;
    private bool isFirstBeat = true;

    void Start()
    {
        // Set the initial scale and heartbeat speed
        initialScale = transform.localScale;
        currentHeartbeatSpeed = normalHeartbeatSpeed;
    }

    void Update()
    {
        // Update heartbeat timing
        heartbeatTime += Time.deltaTime * currentHeartbeatSpeed;

        // Simulate a "lub-dub" heartbeat pattern with a double beat and a pause
        if (isFirstBeat)
        {
            // First beat scale-up and scale-down quickly
            float scaleFactor = Mathf.Lerp(1f, 0f, Mathf.Sin(heartbeatTime * Mathf.PI) * 0.5f + 0.5f);
            transform.localScale = Vector3.Lerp(minScale, maxScale, scaleFactor);

            // Switch to second beat after half of the time period
            if (heartbeatTime >= 0.5f)
            {
                isFirstBeat = false;
                heartbeatTime = 0f;
            }
        }
        else
        {
            // Second beat, a smaller scale-up and down
            float scaleFactor = Mathf.Lerp(1f, 0f, Mathf.Sin(heartbeatTime * Mathf.PI * 0.5f) * 0.5f + 0.5f);
            transform.localScale = Vector3.Lerp(minScale, maxScale, scaleFactor * 0.8f); // Slightly smaller second beat

            // Reset to the first beat after a pause
            if (heartbeatTime >= 1f)
            {
                isFirstBeat = true;
                heartbeatTime = 0f;
            }
        }
    }
}
