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
    //--------------------------------------------------------
    public void Free2Phono() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Free2PhonoCoroutine());
    }
    IEnumerator Free2PhonoCoroutine()
    {
        Cursor.visible = false;
        CamAni.SetTrigger("F2P");
        yield return new WaitForSeconds(1.5f);
        Cursor.visible = true;
    }
    //--------------------------------------------------------
    public void Phono2Free() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Phono2FreeCoroutine());
    }
    IEnumerator Phono2FreeCoroutine()
    {
        Cursor.visible = false;
        CamAni.SetTrigger("P2F");
        yield return new WaitForSeconds(1.5f);
        Cursor.visible = true;
    }

    //-------------------------------------------------------- TUTORIAL 1------------------------------------------------------------------------
    public void Tutorial1() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Tutorial1Coroutine());
    }
    IEnumerator Tutorial1Coroutine()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        AudioManager.Instance.StopAllSounds();
        AudioManager.Instance.PlaySound("Tutorial1");
        //CamAni.SetTrigger("T1");
        yield return new WaitForSeconds(151f);
        GameObject lightGameObject = GameObject.FindGameObjectWithTag("Light");
        if (lightGameObject != null)
        {
            LightManager lightManager = lightGameObject.GetComponent<LightManager>();

            if (lightManager != null)
            {
            LightManager.Instance.StartFlickeringTransitionTo("GameBoard");
            AudioManager.Instance.UnMuffleMusic();
            }
        }
    }
}

