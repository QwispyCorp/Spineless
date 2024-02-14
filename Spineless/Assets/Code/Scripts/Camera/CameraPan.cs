using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPan : MonoBehaviour
{
    public float senseX;
    public float senseY;
    public bool CanLook;
    public Transform orientation;
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private float initialYRotation;
    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;
        initialYRotation = orientation.localRotation.eulerAngles.y;
        Cameralock();
        CanLook = true;
    }

    //-------------------------------------------------------
    public void CamCanLookFalse()
    {
        CanLook = false;
        ResetCamera();
    }

    public void CamCanLookTrue()
    {
        StartCoroutine(CamCanLookCoroutine());
    }

    IEnumerator CamCanLookCoroutine()
    {
        yield return new WaitForSeconds(2f);
        CanLook = true;
    }

    //--------------------------------------------------------
    public void CameraUnlock()
    {
        StartCoroutine(CamUnlockCoroutine());
        ResetCamera();
    }

    IEnumerator CamUnlockCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    //--------------------------------------------------------
    public void Cameralock()
    {
        StartCoroutine(CamLockCoroutine());
        ResetCamera();
    }

    IEnumerator CamLockCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //--------------------------------------------------------
    public void ResetCamera()
    {
        StartCoroutine(ResetCameraCoroutine(0.5f));
    }

    IEnumerator ResetCameraCoroutine(float duration)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.localRotation;
        Vector3 startPosition = transform.localPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.localRotation = Quaternion.Slerp(startRotation, initialRotation, t);
            transform.localPosition = Vector3.Lerp(startPosition, initialPosition, t);
            yield return null;
        }
        transform.localRotation = initialRotation;
        transform.localPosition = initialPosition;
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
    public void ResetCameraRotation()
    {
        transform.localRotation = initialRotation;
        orientation.localRotation = Quaternion.Euler(0, initialYRotation, 0);
    }
}
