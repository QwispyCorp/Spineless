using UnityEngine;

public class CameraPan : MonoBehaviour
{
    private float x;
    private float y;
    public float sensitivity = -0.5f;
    public float maxVerticalAngle = 30f; // Maximum vertical angle in degrees
    public float maxHorizontalAngle = 30f; // Maximum horizontal angle in degrees
    public float deadzoneSize = 500f; // Size of the deadzone in pixels from the center of the screen
    private Vector3 rotate;

    void Update()
    {
        // Get mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Calculate distance from center of screen
        float distanceFromCenter = Vector3.Distance(mousePosition, new Vector3(Screen.width / 2f, Screen.height / 2f));

        // Check if mouse is outside the deadzone
        if (distanceFromCenter > deadzoneSize)
        {
            // Get mouse movement
            y = Input.GetAxis("Mouse X");
            x = Input.GetAxis("Mouse Y");

            // Calculate rotation with sensitivity
            rotate = new Vector3(x, y * sensitivity, 0);

            // Apply rotation to current angles
            Vector3 newAngles = transform.eulerAngles - rotate;

            // Convert angles to be within the range of -180 to 180 degrees
            newAngles.x = newAngles.x > 180 ? newAngles.x - 360 : newAngles.x;
            newAngles.y = newAngles.y > 180 ? newAngles.y - 360 : newAngles.y;

            // Clamp vertical rotation within the specified range
            newAngles.x = Mathf.Clamp(newAngles.x, -maxVerticalAngle, maxVerticalAngle);

            // Clamp horizontal rotation within the specified range
            newAngles.y = Mathf.Clamp(newAngles.y, -maxHorizontalAngle, maxHorizontalAngle);

            // Apply clamped rotation to transform
            transform.eulerAngles = newAngles;
        }
    }
}