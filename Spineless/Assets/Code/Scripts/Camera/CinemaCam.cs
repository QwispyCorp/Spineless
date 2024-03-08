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
        AudioManager.Instance.PlaySound("Footsteps1");
        AudioManager.Instance.PlaySound("CabinetOpen");
        yield return new WaitForSeconds(4.1f);
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
        AudioManager.Instance.PlaySound("Footsteps2");
         yield return new WaitForSeconds(0.9f);
        AudioManager.Instance.PlaySound("CabinetClose");
        yield return new WaitForSeconds(4.1f);
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
        AudioManager.Instance.PlaySound("Footsteps1");
        yield return new WaitForSeconds(1.4f);
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
        AudioManager.Instance.PlaySound("FootstepsFast");
        yield return new WaitForSeconds(1.4f);
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
        AudioManager.Instance.PlaySound("Footsteps1");
        yield return new WaitForSeconds(4f);
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
        AudioManager.Instance.PlaySound("Footsteps2");
        yield return new WaitForSeconds(4.3f);
        Cursor.visible = true;
    }

    //----------------------------CREDITS----------------------------------
    public void Main2Credits() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Main2CreditsCoroutine());
    }
    IEnumerator Main2CreditsCoroutine()
    {
        Cursor.visible = false;
        CamAni.SetTrigger("Credits");
        yield return new WaitForSeconds(2.1f);
        AudioManager.Instance.PlaySound("Footsteps1");
        yield return new WaitForSeconds(3f);
        Cursor.visible = true;
    }
    public void Credits2Main() //CALL THIS TO START THIS ANIMATION
    {
        StartCoroutine(Credits2MainCoroutine());
    }
    IEnumerator Credits2MainCoroutine()
    {
        Cursor.visible = false;
        CamAni.SetTrigger("Main");
        yield return new WaitForSeconds(0.9f);
        AudioManager.Instance.PlaySound("Footsteps2");
        yield return new WaitForSeconds(5f);
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
        //AudioManager.Instance.StopMusicTrack(AudioManager.Instance.CurrentTrack);
        AudioManager.Instance.MuffleMusic();
        AudioManager.Instance.PlaySound("Tutorial1");
        //CamAni.SetTrigger("T1");
        yield return new WaitForSeconds(111f);
        AudioManager.Instance.UnMuffleMusic();
        AudioManager.Instance.StopAllSounds();
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

