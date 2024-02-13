using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPan : MonoBehaviour
{
    public float senseX;
    public float senseY;
    public bool CanLook;
    public Transform orientation;
    private Quaternion initialRotation;
    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        Cameralock();
        CanLook = true;
    }

    public void CamCanLook()
    {
        CanLook = !CanLook;
    }
    public void CameraUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        transform.rotation = Quaternion.identity;
        transform.rotation = initialRotation;
    }

    public void Cameralock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanLook == true)
        {
            // Camera rotation based on mouse input
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
