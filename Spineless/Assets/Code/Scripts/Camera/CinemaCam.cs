using System.Collections;
using UnityEngine;

public class CinemaCam : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;
    public Animator CamAni;
    public CameraPan CamPanScript;
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    //--------------------------------------------------------
    private void Start()
    {
        initialRotation = Cam1.transform.localRotation; 
        initialPosition = Cam1.transform.localPosition;
    }
    //--------------------------------------------------------
    public void ResetCam()
    {
        StartCoroutine(ResetCameraCoroutine(Cam1.transform, 0.5f));
    }

    IEnumerator ResetCameraCoroutine(Transform cameraTransform, float duration)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = cameraTransform.localRotation;
        Vector3 startPosition = cameraTransform.localPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            cameraTransform.localRotation = Quaternion.Slerp(startRotation, initialRotation, t);
            cameraTransform.localPosition = Vector3.Lerp(startPosition, initialPosition, t);
            yield return null;
        }
    }

    //--------------------------------------------------------
    public void Free2Cabinet() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Free2CabinetCoroutine());
    }
    IEnumerator Free2CabinetCoroutine()
    {
        ResetCam();
        CamPanScript.CanLook = false;
        yield return new WaitForSeconds(0.6f);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        CamPanScript.ResetCameraPosition();
        Cam1.SetActive(false);
        Cam2.SetActive(true);
        CamAni.SetTrigger("F2C");
        yield return new WaitForSeconds(1.1f);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    //--------------------------------------------------------
    public void Cabinet2Free() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Cabinet2FreeCoroutine());
    }
    IEnumerator Cabinet2FreeCoroutine()
    {
        Cursor.visible = false;
        CamAni.SetTrigger("C2F");
        yield return new WaitForSeconds(1f);
        Cam1.SetActive(true);
        //CamPanScript.ResetCameraPosition();
        Cam2.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        CamPanScript.CanLook = true;
    }
    //--------------------------------------------------------
    public void Free2board() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Free2BoardCoroutine());
    }
    IEnumerator Free2BoardCoroutine()
    {
        ResetCam();
        CamPanScript.CanLook = false;
        yield return new WaitForSeconds(0.6f);
        //CamPanScript.ResetCameraPosition();
        Cam1.SetActive(false);
        Cam2.SetActive(true);
        CamAni.SetTrigger("F2B");
        yield return new WaitForSeconds(1.1f);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    //--------------------------------------------------------
    public void Board2Free() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Board2FreeCoroutine());
    }
    IEnumerator Board2FreeCoroutine()
    {
        Cursor.visible = false;
        CamAni.SetTrigger("B2F");
        yield return new WaitForSeconds(1f);
        Cam1.SetActive(true);
        Cam2.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSeconds(0.5f);
        CamPanScript.CanLook = true;
    }
}
