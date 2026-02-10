using UnityEngine;

public class ScaleUpEffects : MonoBehaviour
{
    // Adjustable starting and maximum scale sizes
    public Vector3 initialScale = new Vector3(1f, 1f, 1f);
    public Vector3 maxScale = new Vector3(2f, 2f, 2f);
    public float scaleSpeed = 1f;

    void Start()
    {
        // Set the initial scale of the object
        transform.localScale = initialScale;
    }

    void Update()
    {
        // Scale the object up until it reaches maxScale
        if (transform.localScale.x < maxScale.x ||
            transform.localScale.y < maxScale.y ||
            transform.localScale.z < maxScale.z)
        {
            // Smoothly scale the object up by scaleSpeed
            transform.localScale = Vector3.MoveTowards(transform.localScale, maxScale, scaleSpeed * Time.deltaTime);
        }
    }
}