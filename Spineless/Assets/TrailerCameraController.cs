using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float baseMovementSpeed = 5f; // Base speed of camera movement
    public float baseRotationSpeed = 3f; // Base speed of camera rotation
    public float moveSpeedMultiplier = 1f; // Multiplier for speed adjustment
    public float panSpeedMultiplier = 1f;
    private float mouseX;
    private float mouseY;
    public float senseX;
    public float senseY;
    private float xRotation;
    private float yRotation;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Camera movement
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            moveDirection += transform.up;
        else if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
            moveDirection -= transform.up;
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
            moveDirection += transform.right;
        if (Input.GetKey(KeyCode.A))
            moveDirection -= transform.right;

        // Normalize to keep diagonal movement at constant speed
        if (moveDirection != Vector3.zero)
            moveDirection.Normalize();

        transform.position += moveDirection * baseMovementSpeed * moveSpeedMultiplier * Time.deltaTime;

        // Camera rotation
        mouseX = -Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

        yRotation += mouseX * baseRotationSpeed * panSpeedMultiplier;
        xRotation -= mouseY * baseRotationSpeed * panSpeedMultiplier;



        // Adjust speed multiplier using mouse scroll wheel
        if (Input.mouseScrollDelta.y != 0 && Input.GetKey(KeyCode.Space)) //adjust panning speed
        {
            panSpeedMultiplier += Input.mouseScrollDelta.y * 0.1f; // Adjust multiplier by scroll input
            panSpeedMultiplier = Mathf.Clamp(panSpeedMultiplier, 0.0f, 20f); // Clamp multiplier within a reasonable range
        }
        else if (Input.mouseScrollDelta.y != 0) //adjust movement speed
        {
            moveSpeedMultiplier += Input.mouseScrollDelta.y * 0.1f; // Adjust multiplier by scroll input
            moveSpeedMultiplier = Mathf.Clamp(moveSpeedMultiplier, 0.0f, 20f); // Clamp multiplier within a reasonable range
        }

    }
}