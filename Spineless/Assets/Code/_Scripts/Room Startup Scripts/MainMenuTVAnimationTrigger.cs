using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTVAnimationTrigger : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("OnTVDelay");
    }

    void TurnOnTV()
    {
        GetComponent<Animator>().SetTrigger("TurnOn");
    }

    private IEnumerator OnTVDelay()
    {
        yield return new WaitForSeconds(0.5f);
        TurnOnTV();
    }
}
