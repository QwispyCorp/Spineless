using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraPan : MonoBehaviour
{
    private float x;
    private float y;
    public float sensitivity = -0.5f;
    public float maxVerticalAngle = 30f; // Maximum vertical angle in degrees
    public float maxHorizontalAngle = 30f; // Maximum horizontal angle in degrees
    private Vector3 rotate;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Prototype")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (currentScene.name == "GameBoard")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void Update()
    {
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