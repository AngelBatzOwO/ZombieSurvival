using UnityEngine;

public class Aiming : MonoBehaviour
{
    void Update()
    {
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;

        // Adjust for -Y orientation by rotating 90 degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 94f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
