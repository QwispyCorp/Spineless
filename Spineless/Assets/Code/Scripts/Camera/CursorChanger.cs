using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public LayerMask targetLayer; // Layer to check for mouse hovering
    public Texture2D hoverCursor; // Cursor texture when hovering over objects in the target layer
    public Texture2D defaultCursor; // Default cursor texture

    void Update()
    {
        // Create a ray from the camera's position to the mouse cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Perform a raycast to check if the mouse is hovering over an object in the target layer
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
        {
            // Change cursor to hoverCursor if hovering over an object in the target layer
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            // Change cursor to defaultCursor if not hovering over any object in the target layer
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
