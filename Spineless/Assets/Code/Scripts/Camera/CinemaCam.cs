using System.Collections;
using UnityEngine;

public class CinemaCam : MonoBehaviour
{
    public Animator CamAni;
    //--------------------------------------------------------
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Free2Cabinet() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Free2CabinetCoroutine());
    }
    IEnumerator Free2CabinetCoroutine()
    {
        Cursor.visible = false;
        CamAni.SetTrigger("F2C");
        yield return new WaitForSeconds(1.5f);
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
        yield return new WaitForSeconds(1.5f);
        Cursor.visible = true;
    }
    //--------------------------------------------------------
    public void Free2board() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Free2BoardCoroutine());
    }
    IEnumerator Free2BoardCoroutine()
    {
        Cursor.visible = false;
        CamAni.SetTrigger("F2B");
        yield return new WaitForSeconds(1.5f);
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
        yield return new WaitForSeconds(1.5f);
        Cursor.visible = true;
    }
}
