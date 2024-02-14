using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float senseX;
    public float senseY;
    public Transform orientation;
    float xRotation;
    float yRotation;
    public bool CanLook;
    private Quaternion initialRotation;
    private Vector3 initialPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        CanLook = true;
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;
    }
    //--------------------------------------------------------
    void Update()
    {
        if (CanLook == true)
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
    //--------------------------------------------------------
    public void ResetCameraPosition()
    {
        transform.localRotation = initialRotation;
        transform.localPosition = initialPosition;
    }
}