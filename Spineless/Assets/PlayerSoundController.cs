using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource playerChop5;
    [SerializeField] private AudioSource playerChop4;
    [SerializeField] private AudioSource playerChop3;
    [SerializeField] private AudioSource playerChop2;
    [SerializeField] private AudioSource playerChop1;
    [SerializeField] private IntegerReference playerHealth;
    void OnEnable()
    {
        PlayerHealthTest.OnPlayerFingerLost += PlayChopSound;
    }
    void OnDisable()
    {
        PlayerHealthTest.OnPlayerFingerLost -= PlayChopSound;
    }

    void PlayChopSound()
    {
        switch (playerHealth.Value)
        {
            case 1:
                playerChop1.Play();
                break;
            case 2:
                playerChop2.Play();
                break;
            case 3:
                playerChop3.Play();
                break;
            case 4:
                playerChop4.Play();
                break;
            case 5:
                playerChop5.Play();
                break;
            default:
                break;
        }
    }
}
