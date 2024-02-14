using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaCam : MonoBehaviour
{
    //--------------------------------------------------------
    public void CameraUnlock()
    {
        StartCoroutine(CamUnlockCoroutine());
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
    }

    IEnumerator CamLockCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
