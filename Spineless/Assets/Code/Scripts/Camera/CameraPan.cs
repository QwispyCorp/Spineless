using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float senseX;
    public float senseY;
    public Transform orientation;
    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //change to locked when we fix raycast interaction for items and add crosshair/ dot in center of screen

        Cursor.visible = false; //change to false when above is changed to locked
    }
    //--------------------------------------------------------
    void Update()
    {
        {
            float mouseX = -Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}