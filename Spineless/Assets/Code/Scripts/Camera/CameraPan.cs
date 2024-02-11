using UnityEngine;
using System.Collections;


public class CameraPan : MonoBehaviour
{
    public GameObject Cam;
    public Animator animator;
    public float senseX;
    public float senseY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cameralock();
    }

    public void AnimatorOn() // Switches Animator On
    {
        animator.enabled = true;
    }
    public void AnimatorOff() //Switches Animator Off after 1.5 Seconds
    {
        StartCoroutine(TurnOffAnimatorAfterDelay());
    }
    IEnumerator TurnOffAnimatorAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        animator.enabled = false;
    }
    public void CameraUnlock() //Call Function when wnating to use item cabinet
    {
        StartCoroutine(UnlockAfterDelay());
    }
    IEnumerator UnlockAfterDelay() //The time here allows for animations to play
    {
        yield return new WaitForSeconds(1.5f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Cameralock() //Lock for FPS cam
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
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
