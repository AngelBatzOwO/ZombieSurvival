using UnityEngine;
public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // Drag your custom cursor texture here in the Inspector
    public Vector2 hotspot = Vector2.zero; // Adjust to set the cursor's point of interaction
    void Start()
    {
        SetCustomCursor();
    }
    public void SetCustomCursor()
    {
        // Set the cursor with a custom texture and hotspot
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }
    public void ResetCursor()

    {
        // Reset to the default cursor
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}